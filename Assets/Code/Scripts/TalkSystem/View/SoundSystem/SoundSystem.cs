using UnityEngine;

namespace ScriptedTalk
{
    public class SoundSystem : MonoBehaviour, ISoundSystem
    {
        [SerializeField] private AudioSource _bgmSource;
        [SerializeField] private AudioSource _soundEffectSource;

        public void PlaySoundEffect(AudioClip clip)
        {
            _soundEffectSource.clip = clip;
            _soundEffectSource.Play();
        }

        public void PlayBGM(AudioClip clip)
        {
            _bgmSource.clip = clip;
            _bgmSource.Play();
        }

        public void StopBGM()
        {
            _bgmSource.Stop();
        }
    }
}