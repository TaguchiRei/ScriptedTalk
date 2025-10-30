using UnityEngine;

namespace ScriptedTalk.TalkSystem.Entity.Character
{
    public class CharacterData
    {
        public string Name { get; private set; }
        public Texture2D CharacterVisual { get; private set; }
        
        public CharacterData(string name, Texture2D characterVisual)
        {
            Name = name;
            CharacterVisual = characterVisual;
        }
    }
}