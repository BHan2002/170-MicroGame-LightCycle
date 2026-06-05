using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource musicSource;

    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip tileFall;
    [SerializeField] private AudioClip deathCrunch;

    [SerializeField] private AudioClip runnerAudio;

    public void PlayTileFall() => PlaySFX(tileFall);
    public void PlayDeathCrunch() => PlaySFX(deathCrunch);
    public void PlayRunnerAudio() => PlayAmbient(runnerAudio, true);

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Audio clip is null.");
            return;
        }
        sfxSource.clip = clip;
        sfxSource.loop = false;
        sfxSource.Play();
    }

    private void PlayAmbient(AudioClip clip, bool loop = false)
    {
        if (clip == null)
        {
            Debug.LogWarning("Audio clip is null.");
            return;
        }
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }
    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        sfxSource.volume = volume;
    }
}
