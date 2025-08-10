using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static event Action OnToggleMusic;
    public static event Action OnToggleSound;

    [SerializeField] private Button playButton;
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button rulessButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            levelManager.SetNextScene("Tutorial1");
            levelManager.LoadNextScene();
        });
        levelsButton.onClick.AddListener(() =>
        {
            levelManager.SetNextScene("Levels");
            levelManager.LoadNextScene();
        });
        rulessButton.onClick.AddListener(() =>
        {
            levelManager.SetNextScene("Rules");
            levelManager.LoadNextScene();
        });
        musicButton.onClick.AddListener(() =>
        {
            OnToggleMusic?.Invoke();
        });
        soundButton.onClick.AddListener(() =>
        {
            OnToggleSound?.Invoke();
        });
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        AudioManager.OnAudioManagerMusicToggle += OnAudioManagerMusicToggle;
        AudioManager.OnAudioManagerSoundToggle += OnAudioManagerSoundToggle;
        ToggleMusic(AudioManager.GetIsMusicMuted());
        ToggleSound(AudioManager.GetIsSoundMuted());
    }

    private void OnAudioManagerMusicToggle(bool isMuted)
    {
        ToggleMusic(isMuted);
    }

    private void OnAudioManagerSoundToggle(bool isMuted)
    {
        ToggleSound(isMuted);
    }

    private void ToggleMusic(bool isMuted)
    {
        Image image = musicButton.GetComponent<Image>();
        Color color = image.color;
        if (isMuted)
        {
            color.a = .6f;
        }
        else
        {
            color.a = 1f;
        }
        image.color = color;
    }

    private void ToggleSound(bool isMuted)
    {
        Image image = soundButton.GetComponent<Image>();
        Color color = image.color;
        if (isMuted)
        {
            color.a = .6f;
        }
        else
        {
            color.a = 1f;
        }
        image.color = color;
    }

    private void OnDisable()
    {
        AudioManager.OnAudioManagerMusicToggle -= OnAudioManagerMusicToggle;
        AudioManager.OnAudioManagerSoundToggle -= OnAudioManagerSoundToggle;
    }
}
