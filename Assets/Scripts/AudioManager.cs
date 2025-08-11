using System;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public static event Action<bool> OnAudioManagerMusicToggle;
    public static event Action<bool> OnAudioManagerSoundToggle;

    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip colorSplashSound;
    [SerializeField] private AudioClip clockSound;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundAudioSource;

    private static bool isMusicMuted;
    private static bool isSoundMuted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
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
        musicAudioSource.volume = .25f;
        soundAudioSource.volume = .8f;

        musicAudioSource.loop = true;
        musicAudioSource.clip = music;
        musicAudioSource.Play();

        MainMenuManager.OnToggleSound += OnToggleSound;
        MainMenuManager.OnToggleMusic += OnToggleMusic;
        EscapeMenuManager.OnToggleSound += OnToggleSound;
        EscapeMenuManager.OnToggleMusic += OnToggleMusic;
        PlayerController.OnClockCollected += OnClockCollected;
        PlayerController.OnColorSwtiched += OnColorSwitched;
        ButtonClick.OnButtonClicked += OnButtonClicked;
        KeyManager.OnKeyCollected += OnKeyCollected;
    }

    private void OnToggleMusic()
    {
        isMusicMuted = !isMusicMuted;
        if (isMusicMuted)
        {
            musicAudioSource.Pause();
        }
        else
        {
            musicAudioSource.UnPause();
        }
        OnAudioManagerMusicToggle?.Invoke(isMusicMuted);
    }

    private void OnToggleSound()
    {
        isSoundMuted = !isSoundMuted;
        OnAudioManagerSoundToggle?.Invoke(isSoundMuted);
    }

    private void OnClockCollected(Vector3 vector, string color)
    {
        if (isSoundMuted)
        {
            return;
        }
        soundAudioSource.PlayOneShot(clockSound);
    }
    private void OnColorSwitched(Vector3 vector, string arg2)
    {
        if (isSoundMuted)
        {
            return;
        }
        soundAudioSource.PlayOneShot(colorSplashSound);
    }
    private void OnButtonClicked()
    {
        if (isSoundMuted)
        {
            return;
        }
        soundAudioSource.PlayOneShot(buttonClick);
    }
    private void OnKeyCollected()
    {
        if (isSoundMuted)
        {
            return;
        }
        soundAudioSource.PlayOneShot(doorSound);
    }

    public static bool GetIsMusicMuted()
    {
        return isMusicMuted;
    }
    public static bool GetIsSoundMuted()
    {
        return isSoundMuted;
    }
}
