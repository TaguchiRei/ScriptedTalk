using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ScriptedTalk
{
    public class TestDisplayText : MonoBehaviour
    {
        [SerializeField] private TextView _textView;
        [SerializeField] private float _duration;

        private void Start()
        {
            RunTestAsync().Forget();
        }

        private async UniTask RunTestAsync()
        {
            var result = await ServiceLocator.Instance.TryGetSystemAsync<IInputDispatcher>();
            if (result.Item1)
            {
                var dispatcher = result.Item2;
                dispatcher.RegisterActionCancelled(nameof(ActionMaps.Player), nameof(PlayerActions.Attack),
                    _ => _textView.SkipAnimation());

                Debug.Log("getDispatcher");
            }
            else
            {
                Debug.Log("Dispatcher not found");
            }

            _textView.AnimationText("Hello World", (int)(_duration * 1000));
            Debug.Log("complete");
        }
    }
}