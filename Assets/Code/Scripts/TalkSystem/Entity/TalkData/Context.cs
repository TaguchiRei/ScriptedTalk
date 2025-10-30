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

        public bool TryGetNextLine(out TalkLine talkLine)
        {
            var group = TalkGroups[_readingGroup];
            if (group.TryGetNextLine(out var talk))
            {
                talkLine = talk;
                return true;
            }
            else
            {
                talkLine = null;
                return false;
            }
        }

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