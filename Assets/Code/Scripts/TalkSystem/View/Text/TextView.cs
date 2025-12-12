using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ScriptedTalk
{
    public class TextView : MonoBehaviour, ITextView
    {
        public bool TextAnimation { get; private set; }

        [SerializeField] private TextMeshProUGUI _tmp;

        private CancellationTokenSource _cts;

        private void Start()
        {
            DebugGUI.Register("Text Animation",() => TextAnimation);
        }

        public void ShowFullText(string text)
        {
            _tmp.text = text;
        }

        public void AnimationText(string text, int duration)
        {
            ResetCts();
            _cts = new CancellationTokenSource();

            LineUpdate(text, duration, _cts.Token).Forget();
        }

        public void SkipAnimation()
        {
            ResetCts();
        }

        public void ShowTextBox()
        {
            _tmp.gameObject.SetActive(true);
        }

        public void HideTextBox()
        {
            _tmp.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            ResetCts();
        }

        private void ResetCts()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        /// <summary>
        /// 表示文字数の更新を行う
        /// </summary>
        /// <returns></returns>
        private async UniTask LineUpdate(string text, int duration, CancellationToken cancellationToken)
        {
            TextAnimation = true;
            _tmp.maxVisibleCharacters = 0;
            _tmp.text = text;

            var showNum = 0;
            var maxLineNum = text.Length;
            try
            {
                while (showNum < maxLineNum)
                {
                    await UniTask.Delay(duration, cancellationToken: cancellationToken);
                    showNum++;
                    _tmp.maxVisibleCharacters = showNum;
                }
            }
            finally
            {
                _tmp.maxVisibleCharacters = maxLineNum;
            }

            TextAnimation = false;
        }
    }
}