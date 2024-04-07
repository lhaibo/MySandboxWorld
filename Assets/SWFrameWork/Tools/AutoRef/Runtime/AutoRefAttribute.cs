using System;
using UnityEngine;

namespace SWFrameWork.Tools.AutoRef
{
    /// <summary>
    /// 定义一个接口，用于过滤组件
    /// </summary>
    public interface IComponentFilter
    {
        /// <summary>
        /// 检查组件是否满足过滤条件
        /// </summary>
        /// <param name="component">要检查的组件</param>
        /// <returns>如果组件满足过滤条件，则返回true，否则返回false</returns>
        bool Check(Component component);
    }
    
    /// <summary>
    /// AutoRef特性类，用于自动引用组件
    /// </summary>
    public class AutoRefAttribute : PropertyAttribute
    {
        /// <summary>
        /// 定义搜索范围的枚举
        /// </summary>
        public enum SearchScope
        {
            Self,
            Children,
            Parent,
            Anywhere,
            Siblings,   // 兄弟节点上找
            // 可以根据需要添加其他搜索范围
        }

        // 自定义过滤器的属性
        public IComponentFilter Filter { get; private set; }
        // 搜索范围的属性
        public SearchScope Scope { get; private set; }
        // 组件名字的属性
        public string Name { get; private set; }

        /// <summary>
        /// 构造函数，允许设置搜索范围、组件名字和自定义过滤器
        /// </summary>
        /// <param name="scope">搜索范围，默认为Self</param>
        /// <param name="name">组件名字，默认为null</param>
        /// <param name="filterType">自定义过滤器的类型，默认为null</param>
        protected AutoRefAttribute(SearchScope scope = SearchScope.Self, string name = null, Type filterType = null)
        {
            Scope = scope;
            Name = name;
            Filter = filterType != null ? (IComponentFilter)Activator.CreateInstance(filterType) : null;
        }
    }

    /// <summary>
    /// Child特性类，用于自动引用子节点上的组件
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ChildAttribute : AutoRefAttribute
    {
        public ChildAttribute(string name = null,Type filterType = null) : base(SearchScope.Children, name, filterType)
        {
        }
    }

    /// <summary>
    /// Parent特性类，用于自动引用父节点上的组件
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ParentAttribute : AutoRefAttribute
    {
        public ParentAttribute(string name = null,Type filterType = null) : base(SearchScope.Parent, name,filterType)
        {
        }
    }

    /// <summary>
    /// Self特性类，用于自动引用自身上的组件
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SelfAttribute : AutoRefAttribute
    {
        public SelfAttribute(string name = null,Type filterType = null) : base(SearchScope.Self, name, filterType)
        {
        }
    }

    /// <summary>
    /// Anywhere特性类，用于自动引用任何位置上的组件
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AnywhereAttribute : AutoRefAttribute
    {
        public AnywhereAttribute(string name = null,Type filterType = null) : base(SearchScope.Anywhere, name, filterType)
        {
        }
    }
    
    /// <summary>
    /// Sibling特性类，用于自动引用兄弟节点上的组件
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SiblingAttribute : AutoRefAttribute
    {
        public SiblingAttribute(string name = null,Type filterType = null) : base(SearchScope.Siblings, name, filterType)
        {
        }
    }
}