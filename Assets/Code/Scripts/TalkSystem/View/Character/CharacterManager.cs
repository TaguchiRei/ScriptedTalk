using System.Collections.Generic;
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

        private Dictionary<CharacterName, GameObject> _characters;

        private CharacterDataAsset _characterData;

        private async void Start()
        {
            _characterData = await Addressables.LoadAssetAsync<CharacterDataAsset>(_characterDataPath);
            _characters = new Dictionary<CharacterName, GameObject>();
        }

        public void CharacterShow(CharacterName characterName)
        {
            var characterData = _characterData.Characters.SingleOrDefault(d => d.Name == characterName.ToString());
            _characters[characterName] = Instantiate(_characterPrefab, _charactersPanel.transform);
            var character = _characters[characterName].GetComponent<Character>();

            character.Image.sprite = characterData.CharacterImages[0];
        }

        public void CharacterMove(CharacterName characterName, Vector3 position)
        {
            _characters[characterName].GetComponent<RectTransform>().anchoredPosition = position;
        }

        public void AnimationCharacter(CharacterName characterName, string animationName)
        {
            _characters[characterName].GetComponent<Animator>().SetTrigger("Move");
        }
    }
}