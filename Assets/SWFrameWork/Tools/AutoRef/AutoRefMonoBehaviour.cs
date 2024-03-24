using System.Reflection;
using UnityEngine;
using SWFrameWork.Tools.AutoRef;

namespace SWFrameWork.Tools.AutoRef
{
    public class AutoRefMonoBehaviour:MonoBehaviour
    {
#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            this.AutoRefComponents();
        }
#else
        protected virtual void OnValidate() { }
#endif
    }
}