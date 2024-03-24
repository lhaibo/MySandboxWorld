using System;
using UnityEngine;

namespace SWFrameWork.Tools.AutoRef
{
    public class AutoRefAttribute:PropertyAttribute
    {
        public enum SearchScope
        {
            Self,
            Children,
            Parent,
            Anywhere,
            Scene,
            // 可以根据需要添加其他搜索范围
        }

        public SearchScope Scope { get; private set; }
        public string Name { get; private set; }

        // 构造函数允许设置搜索范围和组件名字
        protected AutoRefAttribute(SearchScope scope = SearchScope.Self, string name = null)
        {
            Scope = scope;
            Name = name;
        }
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class ChildAttribute : AutoRefAttribute
    {
        public ChildAttribute(string name = null) : base(SearchScope.Children, name)
        {
        }
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class ParentAttribute : AutoRefAttribute
    {
        public ParentAttribute(string name = null) : base(SearchScope.Parent, name)
        {
        }
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class SelfAttribute : AutoRefAttribute
    {
        public SelfAttribute(string name = null) : base(SearchScope.Self, name)
        {
        }
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class AnywhereAttribute : AutoRefAttribute
    {
        public AnywhereAttribute(string name = null) : base(SearchScope.Anywhere, name)
        {
        }
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class SceneAttribute : AutoRefAttribute
    {
        public SceneAttribute(string name = null) : base(SearchScope.Scene, name)
        {
        }
    }
}