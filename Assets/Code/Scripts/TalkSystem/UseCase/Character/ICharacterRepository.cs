using ScriptedTalk.TalkSystem.Entity.Character;

namespace ScriptedTalk.TalkSystem.UseCase.Character
{
    /// <summary>
    /// キャラクターの取得に使うインターフェース。
    /// </summary>
    public interface ICharacterRepository
    {
        public CharacterData GetCharacter(int characterId);
    }
}