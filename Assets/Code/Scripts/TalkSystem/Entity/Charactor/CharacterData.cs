using UnityEngine;

public class CharacterData
{
    public string Name { get; private set; }
    public Texture2D CharacterVisual { get; private set; }

    CharacterData(string name, Texture2D characterVisual)
    {
        Name = name;
        CharacterVisual = characterVisual;
    }
}