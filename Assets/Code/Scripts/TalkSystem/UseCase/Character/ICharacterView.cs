using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;

namespace ScriptedTalk.TalkSystem.UseCase.Character
{
    /// <summary>
    /// 表示処理を上位レイヤーに移譲
    /// </summary>
    public interface ICharacterView
    {
        public void CharacterShow(int characterId);
        public void CharacterHide(int characterId);
        public void AllCharacterHide();
        public void AnimationPlay(EventData eventData);
    }
}