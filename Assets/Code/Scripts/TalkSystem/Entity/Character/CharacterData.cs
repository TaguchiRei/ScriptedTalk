using UnityEngine;

namespace ScriptedTalk.TalkSystem.Entity.Character
{
    public class CharacterData
    {
        public string Name { get; private set; }
        public int ID { get; private set; }
        public string[] CharacterVisualKey { get; private set; }

        public CharacterData(string name, string[] characterVisualKey)
        {
            Name = name;
            CharacterVisualKey = characterVisualKey;
        }
    }
}