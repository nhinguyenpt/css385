using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _clip) => _audioSource.PlayOneShot(_clip);

    public void StopBackground()
    {
        _audioSource.Stop();
    }
}
