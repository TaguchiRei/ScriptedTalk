using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ScriptedTalk.TalkSystem.UseCase.TextBox
{
    /// <summary>
    /// テキストボックスを管理する
    /// </summary>
    public class TextBox : IDisposable
    {
        public string Text { get; private set; }
        public int VisibleCount { get; private set; }

        /// <summary> 1ms単位で設定 </summary>
        public int ShowTextDuration { get; private set; }

        public bool OnTextFlow { get; private set; }

        /// <summary> テキストが変化したときのデリゲート </summary>
        public event Action OnChangeText;

        public event Action ShowTextBox;
        public event Action HideTextBox;

        private CancellationTokenSource _cts;
        private UniTask _textTask;

        public TextBox(int showTextDuration)
        {
            ShowTextDuration = showTextDuration;
        }

        /// <summary> テキストを設定する </summary>
        public void SetText(string text)
        {
            _cts.Cancel();
            _cts.Dispose();
            Text = text;
            VisibleCount = 0;
            OnChangeText?.Invoke();
            ShowTextBox?.Invoke();
            _cts = new CancellationTokenSource();
            _textTask = TextShow(_cts.Token);
        }

        public void EndTextFlow()
        {
            HideTextBox?.Invoke();
        }

        public void SetVisibleCountMax()
        {
            VisibleCount = Text.Length;
            OnChangeText?.Invoke();
            OnTextFlow = false;
            _cts.Cancel();
            _cts.Dispose();
            _textTask = default;
        }


        private async UniTask TextShow(CancellationToken token)
        {
            OnTextFlow = true;
            while (VisibleCount < Text.Length)
            {
                await UniTask.Delay(ShowTextDuration, cancellationToken: token);
                VisibleCount++;
                OnChangeText?.Invoke();
            }

            OnTextFlow = false;
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts?.Dispose();
            _textTask = default;
        }
    }
}