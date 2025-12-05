using System.Collections.Generic;
using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using ScriptedTalk.TalkSystem.Entity.Character;
using UnityEngine;

namespace ScriptedTalk.TalkSystem.UseCase.Character
{
    /// <summary>
    /// キャラクターの情報取得に使うインターフェース。
    /// </summary>
    public interface ICharacterRepository
    {
        public CharacterEntity GetCharacter(int characterId);

        /// <summary> 登場済みのキャラクターのIDリストを取得する </summary>
        public List<int> GetExistCharactersID();
        
        /// <summary>
        /// キャラクターの位置を取得する
        /// </summary>
        /// <param name="eventEntity"></param>
        /// <returns></returns>
        public Vector3 GetCharacterPosition(EventEntity eventEntity);
        
        /// <summary>
        /// キャラクターのアニメーションを取得する
        /// </summary>
        /// <param name="eventEntity"></param>
        /// <returns></returns>
        public string GetAnimationClip(EventEntity eventEntity);
    }
}