using System;
using UnityEngine;

namespace SWFrameWork.Core.Singleton
{
    /// <summary>
    /// 继承与MonoBehaviour的单例类,用于挂载在场景中的物体上
    /// 所以可以使用mono的函数
    /// </summary>
    public class MonoSingleton<T>:MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                _instance = FindFirstObjectByType<T>();
                if (_instance != null) return _instance;
                
                var go = new GameObject("[Singleton] " +typeof(T).Name);
                _instance = go.AddComponent<T>();

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}