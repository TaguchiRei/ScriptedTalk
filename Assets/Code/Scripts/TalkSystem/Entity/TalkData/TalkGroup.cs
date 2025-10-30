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
        public Dictionary<string, int> Selections { get; private set; }

        public TalkGroup(TalkLine[] talkLines, bool branch, Dictionary<string, int> selections)
        {
            TalkLines = talkLines;
            Branch = branch;
            Selections = selections;
        }
    }
}