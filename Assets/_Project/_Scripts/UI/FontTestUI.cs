using SWFrameWork.Tools.AutoRef;
using TMPro;
using UnityEngine;
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
        
        [SerializeField, Child]
        private TextMeshProUGUI text;
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