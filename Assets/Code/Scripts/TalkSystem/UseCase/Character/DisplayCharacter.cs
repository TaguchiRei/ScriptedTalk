using System.Collections.Generic;
using System.Numerics;
using ScriptedTalk.TalkSystem.Entity.Character;

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

        public void EndTalk()
        {
            _view.AllCharacterHide();
        }

        /// <summary>
        /// 新規にキャラクターを表示する
        /// </summary>
        /// <param name="characterId"></param>
        private void CharacterShow(int characterId, Vector3 position)
        {
            var showCharacter = _repository.GetCharacter(characterId);
            _view.CharacterShow(showCharacter, position);
        }

        private void CharacterShow(CharacterEntity character, Vector3 position)
        {
            _view.CharacterShow(character, position);
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