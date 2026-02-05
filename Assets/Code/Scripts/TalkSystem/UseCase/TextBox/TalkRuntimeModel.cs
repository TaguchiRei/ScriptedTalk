using UnityEngine;

namespace ScriptedTalk
{
    public class TalkRuntimeModel
    {
        private ContextData _contextData;
        private string _readingGroup;
        private int _readingGroupIndex;
        private int _readingLine;

        private bool _lastLine;
        private string[] _selectionGuids;

        public TalkRuntimeModel(ContextData contextData)
        {
            _contextData = contextData;
            _readingGroup = _contextData.StartGroupGuid;
            _readingLine = 0;
            _lastLine = false;
            _readingGroupIndex = GetIndex();
        }

        /// <summary>
        /// 会話の途中から再開するときに読み込む
        /// </summary>
        /// <param name="contextData"></param>
        /// <param name="readingGroup"></param>
        /// <param name="readingLine"></param>
        public TalkRuntimeModel(ContextData contextData, string readingGroup, int readingLine)
        {
            _contextData = contextData;
            _readingGroup = readingGroup;
            _readingLine = readingLine;
            _lastLine = false;

            _readingGroupIndex = GetIndex();
        }

        /// <summary>
        /// 次の行を取得する
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool TryGetNextLine(out TalkLineData line)
        {
            Debug.Log($"_readingGroupIndex: {_readingGroupIndex}, _readingLine: {_readingLine}");
            //最終行に到達あるいは到達済みの時に読んだ時falseを返し、最終行の内容を返す
            if (_contextData.TryGetLine(_readingGroupIndex, _readingLine, out line) && !_lastLine)
            {
                _readingLine++;
                return true;
            }
            else
            {
                _lastLine = true;
                return false;
            }
        }

        public bool IsBranch()
        {
            return _contextData.Context[_readingGroupIndex].IsBranch();
        }

        /// <summary>
        /// 選択肢を取得する
        /// </summary>
        /// <param name="selections"></param>
        /// <returns>選択肢がない場合はfalse</returns>
        public bool TryGetSelection(out string[] selections)
        {
            if (_contextData.TryGetQuestion(_readingGroupIndex, out var questions))
            {
                selections = new string[questions.Count];
                _selectionGuids = new string[questions.Count];
                for (int i = 0; i < questions.Count; i++)
                {
                    selections[i] = questions[i].SelectionTitle;
                    _selectionGuids[i] = questions[i].NextGroupGuid;
                }

                return true;
            }
            else
            {
                selections = null;
                return false;
            }
        }

        /// <summary>
        /// 選択肢によって次のContextに移動する。
        /// GetSelectionメソッドで取得した質問配列のインデックス番号を指定する
        /// </summary>
        /// <param name="selectionIndex"></param>
        public void SelectNextGroup(int selectionIndex)
        {
            _readingGroup = _selectionGuids[selectionIndex];
            _readingGroupIndex = GetIndex();
            _readingLine = 0;
            _lastLine = false;
        }

        private int GetIndex()
        {
            return _contextData.GetGroupIndex(_readingGroup);
        }
    }
}