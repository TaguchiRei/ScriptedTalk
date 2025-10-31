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

        public TalkGroup(TalkLine[] talkLines, bool branch, List<Selection> selections)
        {
            TalkLines = talkLines;
            Branch = branch;
            Selections = selections;
        }

        /// <summary>
        /// 指定した行を取得する。
        /// </summary>
        /// <param name="readingLine"></param>
        /// <param name="talkLine"></param>
        /// <returns>最後の行でfalseを返す</returns>
        public bool TryGetLine(int readingLine, out TalkLine talkLine)
        {
            if (readingLine >= TalkLines.Length)
                throw new ArgumentOutOfRangeException(nameof(readingLine));

            talkLine = TalkLines[readingLine];
            return readingLine < TalkLines.Length - 1;
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