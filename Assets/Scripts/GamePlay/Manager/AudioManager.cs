using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("----------------------AudioSource----------------------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    
    [Header("----------------------AudioClip------------------------")]
    public AudioClip jump;
    public AudioClip die;
    public AudioClip gameOver;
    public AudioClip hitEnemy;
    public AudioClip checkPoint;
    public AudioClip coin;
    public AudioClip star;
    public AudioClip bomb;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void SetMuteSounds()
    {
        if (_Scripts.UI.UIController.Instance.UISetting.IsMuteSound)
        {
            SFXSource.mute = true;
            return;
        }
        SFXSource.mute = false;
    }
    public void SetMuteMusic()
    {
        if (_Scripts.UI.UIController.Instance.UISetting.IsMuteMusic)
        {
            musicSource.mute = true;
            return;
        }
        musicSource.mute = false;
    }

    public void SetVolumeSoundSource(float volume)
    {
        SFXSource.volume = volume;
    }
    public void SetVolumeMusicSource(float value)
    {
        musicSource.volume = value;
    }


    internal void PlaySoundButtonClick()
    {
    }
}
