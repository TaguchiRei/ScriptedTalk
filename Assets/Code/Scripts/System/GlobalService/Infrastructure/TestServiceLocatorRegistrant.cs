using UnityEngine;

namespace ScriptedTalk
{
    public class TestServiceLocatorRegistrant : MonoBehaviour
    {
        [SerializeField] private GameObject _inputDispatcher;


        private void Start()
        {
            ServiceLocator.Instance.TryRegister<IInputDispatcher>(_inputDispatcher.GetComponent<InputDispatcher>());
        }
    }
}