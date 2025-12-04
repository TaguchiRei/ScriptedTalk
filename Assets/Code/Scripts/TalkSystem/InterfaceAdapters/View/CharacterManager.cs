using System;
using System.Collections.Generic;
using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using ScriptedTalk.TalkSystem.Entity.Character;
using ScriptedTalk.TalkSystem.UseCase.Character;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

namespace ScriptedTalk
{
    public class CharacterManager : MonoBehaviour, ICharacterView
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _characterPrefab;

        public int MaxCharacters { get; private set; }

        private GameObject[] _characters;

        /// <summary>
        /// 会話の初めにすべてのキャラクターを作成する
        /// </summary>
        /// <param name="allCharacters"></param>
        public void StartTalk(CharacterData[] allCharacters)
        {
        }


        public void CharacterShow(CharacterData character, Vector3 position)
        {
            var newCharacter = Instantiate(_characterPrefab, _canvas.transform);
            var image = newCharacter.GetComponent<Image>();
            var rect = newCharacter.GetComponent<RectTransform>();
            rect.anchoredPosition = new(position.X, position.Y);
        }

        public void CharacterHide(CharacterData character)
        {
            throw new System.NotImplementedException();
        }

        public void MoveCharacter(CharacterData character, Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void HighLight(List<CharacterData> character)
        {
            throw new System.NotImplementedException();
        }

        public void AllCharacterHide()
        {
            throw new System.NotImplementedException();
        }

        public void AnimationPlay(EventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void AnimationSkip(EventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}