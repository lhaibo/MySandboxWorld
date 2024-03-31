using SWFrameWork.Tools.AutoRef;
using UnityEngine;

namespace SandboxWorld.UI
{
    public class ActiveComponentFilter : IComponentFilter
    {
        public bool Check(Component component)
        {
            return component.gameObject.activeInHierarchy;
        }
    }
}