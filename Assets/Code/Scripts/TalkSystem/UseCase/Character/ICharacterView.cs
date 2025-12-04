using System.Collections.Generic;
using System.Numerics;
using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using ScriptedTalk.TalkSystem.Entity.Character;

namespace ScriptedTalk.TalkSystem.UseCase.Character
{
    /// <summary>
    /// 表示処理を上位レイヤーに移譲
    /// </summary>
    public interface ICharacterView
    {
        public int MaxCharacters { get; }
        public void CharacterShow(CharacterData character, Vector3 position);
        public void CharacterHide(CharacterData character);
        public void MoveCharacter(CharacterData character, Vector3 position);
        public void HighLight(List<CharacterData> character);
        public void AllCharacterHide();
        public void AnimationPlay(EventType eventData);

        public void AnimationSkip(EventType eventData);
    }
}