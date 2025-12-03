using System.Threading;
using Cysharp.Threading.Tasks;
using ScriptedTalk.TalkSystem.Entity.TalkData;
using UnityEngine;

namespace ScriptedTalk.TalkSystem.UseCase.TextBox
{
    /// <summary>
    /// テキストボックスを管理する
    /// </summary>
    public class DisplayTextBox
    {
        private readonly ITalkRepository _talkRepository;
        private readonly ITextBoxView _view;

        public DisplayTextBox(ITalkRepository talkRepository, ITextBoxView view)
        {
            _talkRepository = talkRepository;
            _view = view;
        }

        /// <summary>
        /// 指定された行のテキストの表示を指示する
        /// </summary>
        /// <param name="groupNumber">会話グループ番号</param>
        /// <param name="lineNumber">行番号</param>
        /// <param name="cancellationToken"></param>
        public void DisplayLine(int groupNumber, int lineNumber, CancellationToken cancellationToken)
        {
            var group = _talkRepository.GetTalkGroup(groupNumber);
            var isQuestion = group.TryGetLine(lineNumber, out var line);
            _view.DisplayText(line);
            
            //選択肢がある場合、選択肢を表示する
            if (isQuestion && group.Branch)
            {
                _view.DisplaySelection(group.Selections);
            }

            //表示する文字数をゼロにリセット
            _view.DisplayTextUpdate(0);

            LineUpdate(line, cancellationToken).Forget(Debug.LogException);
        }

        /// <summary>
        /// 表示文字数の更新を行う
        /// </summary>
        /// <returns></returns>
        private async UniTask LineUpdate(TalkLine line, CancellationToken cancellationToken)
        {
            var showNum = 0;
            var maxLineNum = line.Text.Length;
            try
            {
                while (showNum < maxLineNum)
                {
                    await UniTask.Delay(line.TextShowDuration, cancellationToken: cancellationToken);
                    showNum++;
                    _view.DisplayTextUpdate(showNum);
                }
            }
            finally
            {
                _view.DisplayTextUpdate(maxLineNum);
            }
        }
    }
}