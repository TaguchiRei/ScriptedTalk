using System;

namespace ScriptedTalk
{
    public class PlaySoundEvent : IEvent
    {
        public Action EndAction { get; set; }
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Skip()
        {
            throw new NotImplementedException();
        }
    }
}
