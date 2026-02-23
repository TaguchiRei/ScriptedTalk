using System;
using UnityEngine;

namespace ScriptedTalk
{
    [Serializable]
    public class PlaySoundEvent : IEvent, IRequireSoundSystem
    {
        public Action EndAction { get; set; }

        [SerializeField] private AudioClip audioClip;
        private ISoundSystem _soundSystem;


        public void Execute()
        {
            _soundSystem.PlaySoundEffect(audioClip);
        }

        public void Skip()
        {
        }

        public void SetSoundView(ISoundSystem soundSystem)
        {
            _soundSystem = soundSystem;
        }
    }
}