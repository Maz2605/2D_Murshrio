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
}
