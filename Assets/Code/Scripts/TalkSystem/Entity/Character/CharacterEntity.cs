using UnityEngine;

namespace ScriptedTalk.TalkSystem.Entity.Character
{
    public class CharacterEntity
    {
        public string Name { get; private set; }
        public int ID { get; private set; }
        public string CharacterDataKey { get; private set; }

        public CharacterEntity(string name, string characterDataKey)
        {
            Name = name;
            CharacterDataKey = characterDataKey;
        }
    }
}