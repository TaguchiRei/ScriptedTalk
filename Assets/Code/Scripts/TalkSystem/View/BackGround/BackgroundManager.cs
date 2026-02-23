using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptedTalk
{
    public class BackgroundManager : MonoBehaviour, IBackgroundView
    {
        [SerializeField] private GameObject _talkSystemCanvas;
        [SerializeField] private GameObject _backGroundImagePrefab;

        private List<Image> _showTextures;

        private void Start()
        {
            _showTextures = new List<Image>();
        }

        public void ChangeBackGround(Sprite sprite)
        {
        }

        public void ShowBackGroundTexture(Sprite sprite)
        {
            _showTextures.Add(Instantiate(_backGroundImagePrefab, _talkSystemCanvas.transform).GetComponent<Image>());
            _showTextures[^1].sprite = sprite;
            _showTextures[^1].rectTransform.sizeDelta = new Vector2(917, 610);
            _showTextures[^1].rectTransform.anchoredPosition = new Vector2(-519, 229);
            _showTextures[^1].color = new Color(1, 1, 1, 1);
        }

        public void HideBackGroundTexture(Sprite sprite)
        {
            Destroy(_showTextures.SingleOrDefault(s => s.sprite.name == sprite.name)?.gameObject);
        }
    }
}