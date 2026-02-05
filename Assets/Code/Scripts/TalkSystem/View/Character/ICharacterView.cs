using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public interface ICharacterView
{
    void CharacterShow(CharacterName characterName);

    void CharacterMove(CharacterName characterName, Vector3 position);

    void AnimationCharacter(CharacterName characterName, string animationName);
}