using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using SWFrameWork.Tools.Extension;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SWFrameWork.Tools.AutoRef
{
    public static class AutoRefAttributeValidator
    {
#if UNITY_EDITOR
        [MenuItem("CONTEXT/Component/Auto Ref Components")]
        public static void AutoRefComponents(MenuCommand command)
        {
            var component = command.context as Component;
            if (component == null)
            {
                return;
            }

            AutoRef(component);
        }
#endif

        public static void AutoRefComponents(this Component c, bool updateAtRuntime = false)
            => AutoRef(c);


        public static void AutoRef(Component o)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            IList<ReflectionUtil.AttributedField<AutoRefAttribute>> attributedFields =
                new List<ReflectionUtil.AttributedField<AutoRefAttribute>>();
            ReflectionUtil.GetFieldsWithAttributeFromType<AutoRefAttribute>(o.GetType(), attributedFields,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var attributedField in attributedFields)
            {
                var attribute = attributedField.Attribute;
                var field = attributedField.FieldInfo;
                
                if (!field.FieldType.IsSubclassOf(typeof(Component)))
                {
                    Debug.LogError($"AutoRef failed: fieldType:{field.FieldType} variable:<color=red> \"{field.Name}\" </color> is not a Component,will ignore this variable!");
                    continue;
                }

                Component[] components = null;

                switch (attribute.Scope)
                {
                    case AutoRefAttribute.SearchScope.Self:
                        components = new[] {o.GetComponent(field.FieldType)};
                        break;
                    case AutoRefAttribute.SearchScope.Children:
                        components = o.GetComponentsInChildren(field.FieldType, true);
                        break;
                    case AutoRefAttribute.SearchScope.Parent:
                        components = new[] {o.GetComponentInParent(field.FieldType)};
                        break;
                    case AutoRefAttribute.SearchScope.Anywhere:
                        components = GameObject.FindObjectsByType(field.FieldType,FindObjectsSortMode.None) as Component[];
                        break;
                    case AutoRefAttribute.SearchScope.Siblings:
                        components = o.GetSiblingComponents(field.FieldType);
                        break;
                    // 处理其他范围...
                }
                
                // If a filter is specified, use it to filter the components
                if (attribute.Filter != null && components != null)
                {
                    components = Array.FindAll(components, c => attribute.Filter.Check(c));
                }

                // 如果未指定名字，直接使用第一个找到的组件
                if (string.IsNullOrEmpty(attribute.Name) && components != null && components.Length > 0)
                {
                    field.SetValue(o, components[0]);
                    continue;
                }

                // 如果指定了组件名字，确保找到的组件名字匹配
                if (!string.IsNullOrEmpty(attribute.Name) && components != null)
                {
                    foreach (var component in components)
                    {
                        if (component.gameObject.name != attribute.Name)
                        {
                            continue; // 如果名字不匹配，继续搜索下一个
                        }

                        if (component != null && o != component)
                        {
                            field.SetValue(o, component);
#if UNITY_EDITOR
                            EditorUtility.SetDirty(o); // 标记对象为"脏的"，以保存更改
#endif
                            break;
                        }
                    }
                }
            }

            stopwatch.Stop();
            Debug.Log($"{o.name} AutoRef took {stopwatch.Elapsed.TotalMilliseconds} milliseconds.");
        }
    }
}