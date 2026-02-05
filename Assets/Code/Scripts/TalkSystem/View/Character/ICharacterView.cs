using UnityEngine;

public interface ICharacterView
{
    void CharacterShow(CharacterName characterName);

    void CharacterMove(CharacterName characterName, Vector3 position);

    void AnimationCharacter(CharacterName characterName, string animationName);
}