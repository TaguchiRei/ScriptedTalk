using System;
using UnityEngine;

namespace ScriptedTalk
{
    [Serializable]
    public class CharacterShowEvent : IEvent, IRequireCharacterView
    {
        public Action EndAction { get; set; }

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