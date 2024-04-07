using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SWFrameWork.Tools.Extension
{
    public static class TransformExtension
    {
        public static Component[] GetSiblingComponents<T>(this Component cpt) where T : Component
        {
            if (cpt.transform.parent == null)
            {
                return Array.Empty<Component>(); // 如果没有父节点，那么也就没有兄弟节点
            }

            var components = cpt.transform.parent.GetComponentsInChildren<T>();

            return components.Where(component => component.transform != cpt).Cast<Component>().ToArray();
        }
        
        public static Component[] GetSiblingComponents(this Component cpt,Type type)
        {
            if (cpt.transform.parent == null)
            {
                return Array.Empty<Component>(); // 如果没有父节点，那么也就没有兄弟节点
            }

            var components = cpt.transform.parent.GetComponentsInChildren(type);

            return components.Where(component => component.transform != cpt).Cast<Component>().ToArray();
        }
    }
}