using System;

namespace ScriptedTalk
{
    [Serializable]
    public class CharacterShowEvent : IEvent, IRequireCharacterView
    {
        public Action EndAction { get; set; }
        
        private ICharacterView _characterView;

        public void Execute()
        {
        }

        public void Skip()
        {
        }

        public void SetCharacterView(ICharacterView characterView)
        {
        }
    }
}