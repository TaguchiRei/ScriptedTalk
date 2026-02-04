using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterName", menuName = "ScriptableObjects/CharacterData")]
public class CharacterDataAsset : ScriptableObject
{
    public CharacterData[] Characters;
}

[System.Serializable]
public class CharacterData
{
    public string Name;
    public Image[] CharacterImages;
}