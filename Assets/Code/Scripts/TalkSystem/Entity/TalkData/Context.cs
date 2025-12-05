using System;
using System.Collections.Generic;
using ScriptedTalk.TalkSystem.Entity.Character;

namespace ScriptedTalk.TalkSystem.Entity.TalkData
{
    [Serializable]
    public class Context
    {
        public CharacterEntity[] AllCharacters { get; private set; }
        public TalkGroup[] TalkGroups { get; private set; }

        public Context(TalkGroup[] talkGroups)
        {
            TalkGroups = talkGroups;
        }

        /// <summary>
        /// 指定した行を取得する
        /// </summary>
        /// <param name="readingGroup"></param>
        /// <param name="readingLine"></param>
        /// <param name="talkLine"></param>
        /// <returns>falseを返したときはグループの文末</returns>
        public bool TryGetLine(int readingGroup, int readingLine, out TalkLine talkLine)
        {
            return TalkGroups[readingGroup].TryGetLine(readingLine, out talkLine);
        }

        /// <summary>
        /// 質問の選択肢を取得する
        /// </summary>
        /// <param name="readingGroup"></param>
        /// <param name="selection"></param>
        /// <returns>trueなら質問、falseを返したときは文末</returns>
        public bool TryGetQuestion(int readingGroup, out List<TalkGroup.Selection> selection)
        {
            if (TalkGroups[readingGroup].Branch)
            {
                selection = TalkGroups[readingGroup].Selections;
                return true;
            }
            else
            {
                selection = null;
                return false;
            }
        }

        /// <summary>
        /// 選択肢を決定した際のメソッド。次のグループを取得する
        /// </summary>
        /// <param name="readingGroup"></param>
        /// <param name="selection"></param>
        public int SelectSelection(int readingGroup, string selection)
        {
            var group = TalkGroups[readingGroup];

            if (group.Branch)
            {
                var index = group.Selections.FindIndex(s => s.SelectionTitle == selection);

                return group.Selections[index].NextGroupID;
            }
            else
            {
                return 0;
            }
        }
    }
}