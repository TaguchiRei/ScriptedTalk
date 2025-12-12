using System;
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
        private readonly ITextBoxPresenter _presenter;

        public DisplayTextBox(ITalkRepository talkRepository, ITextBoxPresenter presenter)
        {
            _talkRepository = talkRepository;
            _presenter = presenter;
        }

        /// <summary>
        /// 指定された行のテキストの表示を指示する
        /// </summary>
        /// <param name="groupNumber">会話グループ番号</param>
        /// <param name="lineNumber">行番号</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public bool DisplayLine(int groupNumber, int lineNumber, CancellationToken cancellationToken)
        {
            var group = _talkRepository.GetTalkGroup(groupNumber);
            var isQuestion = group.TryGetLine(lineNumber, out var line);
            _presenter.DisplayText(line);

            //選択肢がある場合、選択肢を表示する
            if (isQuestion && group.IsBranch())
            {
                _presenter.DisplaySelection(group.Selections);
                return false;
            }

            return true;
        }
    }
}