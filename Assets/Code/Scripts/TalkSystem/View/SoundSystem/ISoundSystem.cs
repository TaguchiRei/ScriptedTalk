using UnityEngine;

public interface ISoundSystem
{
    public void PlaySoundEffect(AudioClip clip);

    public void PlayBGM(AudioClip clip);

    public void StopBGM();
}