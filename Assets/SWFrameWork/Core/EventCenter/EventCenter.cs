using System.Collections.Generic;
using UnityEngine;

namespace SWFrameWork.Core.EventCenter
{
    public static class EventCenter<T> where T : IEvent
    {
        static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public static void Register(EventBinding<T> binding) => bindings.Add(binding);
        public static void Deregister(EventBinding<T> binding) => bindings.Remove(binding);

        /// <summary>
        /// 触发监听此事件的所有方法
        /// </summary>
        /// <param name="event"></param>
        public static void Fire(T @event)
        {
            foreach (var binding in bindings)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }
        
        /// <summary>
        /// 触发监听此事件的所有无参方法
        /// </summary>
        public static void Fire()
        {
            foreach (var binding in bindings)
            {
                binding.OnEventNoArgs.Invoke();
            }
        }
        
        static void Clear()
        {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            bindings.Clear();
        }
    }
}