using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ScriptedTalk
{
    public class TextPanelView : MonoBehaviour, ITextView
    {
        /// <summary> 会話中かどうか </summary>
        [ShowOnly]
        public bool IsTalking { get; private set; }

        /// <summary> テキストがアニメーション中かどうか </summary>
        [ShowOnly]
        public bool TextAnimation { get; private set; }

        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private TextMeshProUGUI _mainText;
        [SerializeField] private GameObject _namePanel;
        [SerializeField] private GameObject _textPanel;

        [Header("必要インターフェース群")] [SerializeField]
        private BackgroundManager _backgroundView;

        [SerializeField] private CharacterManager _characterView;
        [SerializeField] private EffectManager _effectView;

        private UniTask _talkTask;
        private CancellationTokenSource _cts;
        private TalkRunner _talkRunner;
        private TalkRuntimeModel _talkRuntimeModel;

        public void StartTalking(string assetPath)
        {
            if (IsTalking) return;
            IsTalking = true;

            _textPanel.SetActive(true);
            _characterNameText.text = string.Empty;
            _mainText.text = string.Empty;

            //ToDo ユースケースのインスタンス化等を行う
            //_talkRuntimeModel = new TalkRuntimeModel();
        }

        public async UniTask TalkAsync(string characterName, string text, int textShowSpeed, CancellationToken ct)
        {
            TextAnimation = true;
            _namePanel.SetActive(text != string.Empty);
            _characterNameText.text = characterName;
            _mainText.text = text;
            _mainText.maxVisibleCharacters = 0;

            var waitTime = 1f / textShowSpeed * 1000f;
            var startTime = Time.time;

            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    _mainText.maxVisibleCharacters = (int)((Time.time - startTime) / waitTime);
                    await UniTask.Yield(ct);
                }
            }
            finally
            {
                _mainText.maxVisibleCharacters = text.Length;
                TextAnimation = false;
            }
        }


        public void ShowFullText(string characterName, string text)
        {
            if (TextAnimation)
            {
                SkipAnimation();
            }

            _mainText.maxVisibleCharacters = text.Length;
            _mainText.text = text;
        }

        public void AnimationText(string characterName, string text, int textShowSpeed)
        {
            if (TextAnimation)
            {
                Debug.LogWarning("テキストアニメーション再生中にテキストが更新されました。\n意図していない場合は修正してください");
                SkipAnimation();

                ResetCancellationTokenSource();

                _talkTask = TalkAsync(characterName, text, textShowSpeed, _cts.Token);
            }
        }

        public void SkipAnimation()
        {
            if (TextAnimation)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        public void ShowTextBox()
        {
            _textPanel.SetActive(true);
            _characterNameText.text = string.Empty;
            _mainText.text = string.Empty;
        }

        public void HideTextBox()
        {
            _textPanel.SetActive(false);
            _namePanel.SetActive(false);

            _characterNameText.text = string.Empty;
            _mainText.text = string.Empty;
        }

        private void ResetCancellationTokenSource()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }

            _cts = new CancellationTokenSource();
        }
    }
}