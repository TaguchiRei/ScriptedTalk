using System;
using UnityEngine;

namespace ScriptedTalk
{
    [Serializable]
    public class CharacterShowEvent : IEvent, IRequireCharacterView
    {
        [SerializeField] private CharacterName _characterName;
        public Action EndAction { get; set; }

        private ICharacterView _characterView;

        public void Execute()
        {
            _characterView.CharacterShow(_characterName);
        }

        public void Skip()
        {
        }

        public void SetCharacterView(ICharacterView characterView)
        {
            _characterView = characterView;
        }
    }
}