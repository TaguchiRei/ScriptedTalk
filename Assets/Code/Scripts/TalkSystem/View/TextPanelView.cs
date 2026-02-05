using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

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
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private SelectionManager _selectionView;

        private UniTask _talkTask;
        private CancellationTokenSource _cts;
        private TalkRunner _talkRunner;
        private TalkRuntimeModel _talkRuntimeModel;
        private Action _talkEndAction;

        public async UniTask StartTalking(string assetPath, Action talkEndAction = null)
        {
            TextAnimation = false;
            if (IsTalking) return;
            Debug.Log("StartTalking");
            IsTalking = true;

            _textPanel.SetActive(true);
            _characterNameText.text = string.Empty;
            _mainText.text = string.Empty;

            var result = await Addressables.LoadAssetAsync<ContextData>(assetPath);

            _talkRuntimeModel = new TalkRuntimeModel(result);
            _talkRunner = new(
                _talkRuntimeModel,
                this,
                _selectionView,
                _backgroundView,
                _characterView,
                _effectView,
                _soundSystem);
            var dis = await ServiceLocator.Instance.TryGetSystemAsync<IInputDispatcher>();
            if (dis.Item1)
            {
                dis.Item2.SwitchActionMap(nameof(ActionMaps.UI));
                dis.Item2.RegisterActionCancelled(nameof(ActionMaps.UI), nameof(UIActions.Click), OnClick);
                Debug.Log("LoadSucsess");
            }
            else
            {
                Debug.Log("LoadFail");
            }

            _talkEndAction = talkEndAction;
            _talkRunner.OnNextButtonInput();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            var state = _talkRunner.OnNextButtonInput();
            Debug.Log(state);
            switch (state)
            {
                case TalkRunner.TalkState.EndTalk:
                    HideTextBox();
                    _talkEndAction?.Invoke();
                    break;
                case TalkRunner.TalkState.Question:
                    if (_talkRuntimeModel.TryGetSelection(out var selection))
                    {
                        Debug.Log("Get Selections");
                        _selectionView.ShowSelection(selection, i =>
                        {
                            _talkRunner.AnsweredQuestion(i);

                            OnClick(default);
                        });
                    }

                    break;
                case TalkRunner.TalkState.Talking:
                case TalkRunner.TalkState.DisableOperation:
                    break;
            }
        }

        public async UniTask TalkAsync(string characterName, string text, float textShowSpeed, CancellationToken ct)
        {
            TextAnimation = true;
            _namePanel.SetActive(text != string.Empty);
            _characterNameText.text = characterName;
            _mainText.text = text;
            _mainText.maxVisibleCharacters = 0;

            var startTime = Time.time;

            try
            {
                while (_mainText.maxVisibleCharacters < text.Length)
                {
                    _mainText.maxVisibleCharacters = (int)((Time.time - startTime) / textShowSpeed);
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

        public void AnimationText(string characterName, string text, float textShowSpeed)
        {
            if (TextAnimation)
            {
                Debug.LogWarning("テキストアニメーション再生中にテキストが更新されました。\n意図していない場合は修正してください");
                SkipAnimation();

                ResetCancellationTokenSource();

                _talkTask = TalkAsync(characterName, text, textShowSpeed, _cts.Token);
            }
            else
            {
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
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts.Dispose();
            }

            _cts = new CancellationTokenSource();
        }
    }
}