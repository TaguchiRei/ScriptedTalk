using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ScriptedTalk
{
    public class TextPanelView : MonoBehaviour, ITextView
    {
        /// <summary> 会話中かどうか </summary>
        public bool IsTalking { get; private set; }

        /// <summary> テキストがアニメーション中かどうか </summary>
        public bool TextAnimation { get; private set; }

        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private TextMeshProUGUI _mainText;
        [SerializeField] private GameObject _namePanel;
        [SerializeField] private GameObject _textPanel;

        private UniTask _talkTask;

        public void StartTalking(string assetPath)
        {
            if (IsTalking) return;
            IsTalking = true;

            _textPanel.SetActive(true);
            _characterNameText.text = string.Empty;
            _mainText.text = string.Empty;

            //ToDo ユースケースのインスタンス化等を行う
        }

        public async UniTask TalkAsync(string characterName, string text, int textShowSpeed, CancellationToken ct)
        {
            TextAnimation = true;
            _namePanel.SetActive(text != string.Empty);
            _mainText.text = text;
            _mainText.maxVisibleCharacters = 0;

            var waitTime = 1f / textShowSpeed * 1000f;
            var startTime = Time.time;

            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    _mainText.maxVisibleCharacters = (int)((Time.time - startTime) / waitTime);
                    await UniTask.Yield();
                }
            }
            finally
            {
                _mainText.maxVisibleCharacters = text.Length;
                TextAnimation = false;
            }
        }


        public void ShowFullText(string text)
        {
            throw new System.NotImplementedException();
        }

        public void AnimationText(string text, int duration)
        {
            throw new System.NotImplementedException();
        }

        public void SkipAnimation()
        {
            throw new System.NotImplementedException();
        }

        public void ShowTextBox()
        {
            throw new System.NotImplementedException();
        }

        public void HideTextBox()
        {
            throw new System.NotImplementedException();
        }
    }
}