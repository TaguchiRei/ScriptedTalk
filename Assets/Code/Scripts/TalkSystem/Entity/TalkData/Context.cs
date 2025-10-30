using System;

namespace ScriptedTalk.TalkSystem.Entity.TalkData
{
    [Serializable]
    public class Context
    {
        public TalkGroup[] TalkGroups { get; private set; }

        public Context(TalkGroup[] talkGroups)
        {
            TalkGroups = talkGroups;
        }
    }
}