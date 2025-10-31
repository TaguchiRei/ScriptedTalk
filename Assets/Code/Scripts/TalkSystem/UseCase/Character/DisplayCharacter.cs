using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;

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

        public async UniTask CharacterEvent(CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    
                }
            }
            finally
            {
                
            }
        } 

        /// <summary>
        /// すべてのイベントを実行する
        /// </summary>
        /// <param name="events"></param>
        private void ExecuteAllEvent(List<EventData> events)
        {
            var exists = _repository.GetExistCharactersID();
            foreach (var eventData in events)
            {
                if (eventData.EventID < 0) continue;
                var characterID = eventData.CharacterID;
                if (!exists.Contains(characterID))
                {
                    CharacterShow(characterID);
                }

                _view.AnimationPlay(eventData.CharacterID, eventData.EventID);
            }
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

        /// <summary>
        /// キャラクターを隠す
        /// </summary>
        /// <param name="characterId"></param>
        private void HideCharacter(int characterId)
        {
            var hideCharacter = _repository.GetCharacter(characterId);
            _view.CharacterHide(hideCharacter);
        }
    }
}