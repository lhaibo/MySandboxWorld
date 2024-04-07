using System;
using UnityEngine;

namespace SWFrameWork.Tools.AutoRef
{
    public interface IComponentFilter
    {
        bool Check(Component component);
    }
    
    public class AutoRefAttribute : PropertyAttribute
    {
        public enum SearchScope
        {
            Self,
            Children,
            Parent,
            Anywhere,
            Scene,
            Siblings,   // 兄弟节点上找
            // 可以根据需要添加其他搜索范围
        }

        // Add a delegate type property for custom filter
        public IComponentFilter Filter { get; private set; }
        
        public SearchScope Scope { get; private set; }
        public string Name { get; private set; }

        // 构造函数允许设置搜索范围和组件名字,以及自定义过滤器
        protected AutoRefAttribute(SearchScope scope = SearchScope.Self, string name = null, Type filterType = null)
        {
            Scope = scope;
            Name = name;
            Filter = filterType != null ? (IComponentFilter)Activator.CreateInstance(filterType) : null;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ChildAttribute : AutoRefAttribute
    {
        public ChildAttribute(string name = null,Type filterType = null) : base(SearchScope.Children, name, filterType)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ParentAttribute : AutoRefAttribute
    {
        public ParentAttribute(string name = null,Type filterType = null) : base(SearchScope.Parent, name,filterType)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class SelfAttribute : AutoRefAttribute
    {
        public SelfAttribute(string name = null,Type filterType = null) : base(SearchScope.Self, name, filterType)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class AnywhereAttribute : AutoRefAttribute
    {
        public AnywhereAttribute(string name = null,Type filterType = null) : base(SearchScope.Anywhere, name, filterType)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class SceneAttribute : AutoRefAttribute
    {
        public SceneAttribute(string name = null,Type filterType = null) : base(SearchScope.Scene, name, filterType)
        {
        }
    }
    
    // 添加新的特性类
    [AttributeUsage(AttributeTargets.Field)]
    public class SiblingAttribute : AutoRefAttribute
    {
        public SiblingAttribute(string name = null,Type filterType = null) : base(SearchScope.Siblings, name, filterType)
        {
        }
    }
}