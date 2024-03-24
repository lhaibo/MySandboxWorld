﻿using System.Reflection;
using UnityEditor;
using UnityEngine;

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

        private static void AutoRef(Component o)
        {
            var fields = o.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute<AutoRefAttribute>();
                if (attribute != null)
                {
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
                        // 处理其他范围...
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
            }
        }
    }
}