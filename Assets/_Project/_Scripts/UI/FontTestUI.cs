using SWFrameWork.Core.EventCenter;
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
        
        [SerializeField, Child(filterType:typeof(ActiveComponentFilter))]
        private TextMeshProUGUI text;
        // 错误使用Self，因为GameObject不是一个Component
        //[Self()]private GameObject go;
        
        public int a;
        private EventBinding<TestEvents> _testEventBinding;
        private EventBinding<TestEvents2> _testEventBinding2;

        private void Start()
        {
            button.onClick.AddListener(OnClick);
            button3.onClick.AddListener(()=>
            {
                Application.Quit(0);
            });
        }

        private void OnEnable()
        {
            _testEventBinding = new EventBinding<TestEvents>(OnTestEventWithArgs);
            
            EventCenter<TestEvents>.Register(_testEventBinding);            
            
            _testEventBinding2 = new EventBinding<TestEvents2>(OnTestEvent);
            EventCenter<TestEvents2>.Register(_testEventBinding2);
        }

        void OnDisable()
        {
            EventCenter<TestEvents>.Deregister(_testEventBinding);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                EventCenter<TestEvents>.Fire(new TestEvents(number:5,text:"Hello World"));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                EventCenter<TestEvents2>.Fire();
            }
            
        }

        private void OnClick()
        {
            Debug.Log("Button Clicked");
        }
        
        void OnTestEventWithArgs(TestEvents testEvent)
        {
            Debug.Log("Test Event Fired with args: " + testEvent.number + " " + testEvent.text);
        }
        
        void OnTestEvent()
        {
            Debug.Log("Test Event Fired no args");
        }        
    }

    struct TestEvents : IEvent
    {
        public int number;
        public string text;

        public TestEvents(int number, string text)
        {
            this.number = number;
            this.text = text;
        }
    }
    
    struct TestEvents2 : IEvent
    {
    }
}