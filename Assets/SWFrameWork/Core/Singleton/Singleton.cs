namespace SWFrameWork.Core.Singleton
{
    /// <summary>
    /// 单例基类
    /// </summary>
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;

        public static T Instance => _instance ??= new T();

        protected Singleton()
        {
            Init();
        }

        protected virtual void Init()
        {
        }
    }
}