using System;
using System.Collections.Generic;

namespace ScriptedTalk.TalkSystem.Entity.TalkData
{
    /// <summary>
    /// 前回の続きまたは会話の開始から次の分岐または終了までの単位
    /// </summary>
    [Serializable]
    public class TalkGroup
    {
        /// <summary> 会話の内容 </summary>
        public TalkLine[] TalkLines { get; private set; }
        /// <summary> 分岐かどうか </summary>
        public bool Branch { get; private set; }
        /// <summary> 選択肢と次にどのTalkGroupにつながるか </summary>
        public List<Selection> Selections { get; private set; }

        private int _readingLine = -1;

        public TalkGroup(TalkLine[] talkLines, bool branch, List<Selection> selections)
        {
            TalkLines = talkLines;
            Branch = branch;
            Selections = selections;
        }

        public bool TryGetNextLine(out TalkLine talkLine)
        {
            _readingLine++;
            if (_readingLine < TalkLines.Length)
            {
                talkLine = TalkLines[_readingLine];
                return true;
            }
            else
            {
                talkLine = null;
                return false;
            }
        }

        public TalkLine GetLineForIndex(int index)
        {
            return TalkLines[index];
        }
        

        [Serializable]
        public class Selection
        {
            public string SelectionTitle { get; private set; }
            public int NextGroupID { get; private set; }

            public Selection(string selectionTitle, int nextGroupID)
            {
                SelectionTitle = selectionTitle;
                NextGroupID = nextGroupID;
            }
        }
    }
}