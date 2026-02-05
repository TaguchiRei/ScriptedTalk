using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ScriptedTalk
{
    public class CharacterManager : MonoBehaviour, ICharacterView
    {
        [SerializeField] private string _characterDataPath;
        [SerializeField] private GameObject _characterPrefab;
        [SerializeField] private GameObject _charactersPanel;

        private GameObject[] _characters;

        private CharacterDataAsset _characterData;

        private async void Start()
        {
            _characterData = await Addressables.LoadAssetAsync<CharacterDataAsset>(_characterDataPath);
        }

        public void CharacterShow(CharacterName characterName)
        {
            var character = _characterData.Characters.SingleOrDefault(d => d.Name == characterName.ToString());
            Instantiate(_characterPrefab, _charactersPanel.transform);
        }

        public void CharacterMove(CharacterName characterName, Vector3 position)
        {
        }

        public void AnimationCharacter(CharacterName characterName, string animationName)
        {
        }
    }
}