using System;
using System.Collections.Generic;

namespace ScriptedTalk.TalkSystem.Entity.TalkData
{
    [Serializable]
    public class Context
    {
        public TalkGroup[] TalkGroups { get; private set; }

        private int _readingGroup = 0;

        public Context(TalkGroup[] talkGroups)
        {
            TalkGroups = talkGroups;
        }

        /// <summary>
        /// 次の行を取得する。
        /// </summary>
        /// <param name="talkLine"></param>
        /// <returns>falseを返したときはグループの文末</returns>
        public bool TryGetNextLine(out TalkLine talkLine)
        {
            return TalkGroups[_readingGroup].TryGetNextLine(out talkLine);
        }

        /// <summary>
        /// 質問の選択肢を取得する
        /// </summary>
        /// <param name="selection"></param>
        /// <returns>trueなら質問、falseを返したときは文末</returns>
        public bool TryGetQuestion(out List<TalkGroup.Selection> selection)
        {
            if (TalkGroups[_readingGroup].Branch)
            {
                selection = TalkGroups[_readingGroup].Selections;
                return true;
            }
            else
            {
                selection = null;
                return false;
            }
        }

        /// <summary>
        /// 選択肢を決定した際のメソッド。次のグループに移動する
        /// </summary>
        /// <param name="selection"></param>
        public void SelectSelection(string selection)
        {
            var group = TalkGroups[_readingGroup];
            
            if (group.Branch)
            {
                var index = group.Selections.FindIndex(s => s.SelectionTitle == selection);
                _readingGroup = group.Selections[index].NextGroupID;
            }
        }
    }
}