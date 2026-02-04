using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

        private CharacterDataAsset[] _characterDataCash;
        private GameObject[] _characters;

        


        public void CharacterShow(CharacterEntity character)
        {
            
        }

        public void CharacterShow(CharacterEntity character, Vector3 position)
        {
        }

        public void CharacterHide(CharacterEntity character)
        {
            
        }

        public void MoveCharacter(CharacterEntity character, Vector3 position)
        {
        }

        public void HighLight(List<CharacterEntity> character)
        {
        }

        public void AllCharacterHide()
        {
        }

        private async UniTask FadeImage(Image image, float start, float end, float duration)
        {
            
        }

        private void CharacterGenerate()
        {
            var newCharacter = Instantiate(_characterPrefab, _canvas.transform);
            var rect = newCharacter.GetComponent<RectTransform>();
            rect.anchoredPosition = new(0, 0);

            var image = newCharacter.GetComponent<Image>();
            image.color = new Color(0, 0, 0, 0);
        }
    }
}