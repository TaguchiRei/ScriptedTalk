using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ScriptedTalk
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private TextPanelView _textPanelView;
        private const string address = @"Assets/Data/ScriptaleObjects/TestContextData.asset";

        private UniTask _task;

        public void StartTalkEvent()
        {
            _task = _textPanelView.StartTalking(address);
        }

        private void Update()
        {
            if (_task.Status == UniTaskStatus.Canceled)
            {
            }
        }
    }
}