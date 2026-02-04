using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptedTalk
{
    public class SelectionManager : MonoBehaviour, ISelectionView
    {
        [SerializeField] GameObject _selectionPrefab;

        private GameObject[] _selectionInstance;

        public void ShowSelection(string[] selections)
        {
            _selectionInstance = new GameObject[selections.Length];
            for (int i = 0; i < selections.Length; i++)
            {
                _selectionInstance[i] = Instantiate(_selectionPrefab, transform);
                var text = _selectionInstance[i].GetComponentInChildren<TextMeshProUGUI>();
                text.text = selections[i];

                var button = _selectionInstance[i].GetComponentInChildren<Button>();
                var i1 = i;
                button.onClick.AddListener(() => SelectAny(i1));
            }
        }

        private void SelectAny(int index)
        {
            foreach (var instance in _selectionInstance)
            {
                Destroy(instance.gameObject);
            }
            
        }
    }
}