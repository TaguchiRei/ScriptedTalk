using System.Collections.Generic;
using ScriptedTalk.TalkSystem.Entity.Character;

namespace ScriptedTalk.TalkSystem.UseCase.Character
{
    /// <summary>
    /// キャラクターの取得に使うインターフェース。
    /// </summary>
    public interface ICharacterRepository
    {
        public CharacterData GetCharacter(int characterId);

        /// <summary> 登場済みのキャラクターのIDリストを取得する </summary>
        public List<int> GetExistCharactersID();
    }
}