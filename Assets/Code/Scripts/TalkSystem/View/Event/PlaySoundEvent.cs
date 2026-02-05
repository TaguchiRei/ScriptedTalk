using System;

namespace ScriptedTalk
{
    [Serializable]
    public class PlaySoundEvent : IEvent, IRequireSoundSystem
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

        public void SetSoundView(ISoundSystem soundSystem)
        {
            throw new NotImplementedException();
        }
    }
}