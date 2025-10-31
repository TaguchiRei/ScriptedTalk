using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using ScriptedTalk.TalkSystem.Entity.Character;
using UnityEngine;

namespace ScriptedTalk.TalkSystem.UseCase.Character
{
    public class DisplayCharacter
    {
        private readonly ICharacterView _view;
        private readonly ICharacterRepository _repository;

        public DisplayCharacter(ICharacterView view, ICharacterRepository repository)
        {
            _view = view;
            _repository = repository;
        }

        public void StartTalk(CharacterData[] firstCharacters)
        {
            foreach (var firstCharacter in firstCharacters)
            {
                CharacterShow(firstCharacter);
            }
        }

        public void EndTalk()
        {
            _view.AllCharacterHide();
        }

        /// <summary>
        /// すべてのイベントを実行する
        /// </summary>
        /// <param name="events"></param>
        public void ExecuteAllEvent(List<EventData> events)
        {
            var exists = _repository.GetExistCharactersID();
            List<CharacterData> characters = new();
            foreach (var eventData in events)
            {
                if (eventData.EventID < 0) continue;
                var characterID = eventData.CharacterID;
                if (!exists.Contains(characterID))
                {
                    CharacterShow(characterID);
                }
                characters.Add(_repository.GetCharacter(characterID));
                _view.AnimationPlay(eventData);
            }
            _view.HighLight(characters);
        }

        /// <summary>
        /// 新規にキャラクターを表示する
        /// </summary>
        /// <param name="characterId"></param>
        private void CharacterShow(int characterId)
        {
            var showCharacter = _repository.GetCharacter(characterId);
            _view.CharacterShow(showCharacter);
        }

        private void CharacterShow(CharacterData character)
        {
            _view.CharacterShow(character);
        }

        /// <summary>
        /// キャラクターを隠す
        /// </summary>
        /// <param name="characterId"></param>
        private void CharacterHide(int characterId)
        {
            var hideCharacter = _repository.GetCharacter(characterId);
            _view.CharacterHide(hideCharacter);
        }
    }
}