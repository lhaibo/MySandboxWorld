using System;
//using KBCore.Refs;
using SWFrameWork.Tools.AutoRef;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SandboxWorld.UI
{
    public class FontTestUI : AutoRefMonoBehaviour
    {
        [SerializeField, Child(name: "Button")]
        private Button button;

        [SerializeField, Child(name: "Button2")]
        private Button button2;

        [SerializeField, Child(name: "Button3")]
        private Button button3;

        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Debug.Log("Button Clicked");
        }
    }
}