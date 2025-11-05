using GamesKeystoneFramework.Attributes;
using UnityEngine;

namespace ScriptedTalk
{
    public class Test : MonoBehaviour
    {
        [SerializeField,StringLengthLimit(5)] private string _st;
    }
}
