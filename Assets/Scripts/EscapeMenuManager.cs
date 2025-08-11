using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenuManager : MonoBehaviour
{
    public static EscapeMenuManager Instance { get; private set; }

    public static event Action OnToggleMusic;
    public static event Action OnToggleSound;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button exitButton;

    private CanvasGroup canvasGroup;
    private InputAction EscClick;
    private bool isVisible;

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
        isVisible = false;
        canvasGroup = GetComponent<CanvasGroup>();

        restartButton.onClick.AddListener(() =>
        {
            HideEscMenu();
            LevelManager levelManager = FindFirstObjectByType<LevelManager>();
            levelManager.SetNextScene(SceneManager.GetActiveScene().name);
            levelManager.LoadNextScene();
        });
        menuButton.onClick.AddListener(() =>
        {
            HideEscMenu();
            LevelManager levelManager = FindFirstObjectByType<LevelManager>();
            levelManager.SetNextScene("MainMenu");
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

        EscClick = InputSystem.actions.FindAction("EscClick");
        EscClick.performed += ToggleEscMenu;
        AudioManager.OnAudioManagerMusicToggle += OnAudioManagerMusicToggle;
        AudioManager.OnAudioManagerSoundToggle += OnAudioManagerSoundToggle;
    }

    private void ToggleEscMenu(InputAction.CallbackContext context)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.Equals("MainMenu") || currentScene.Equals("Levels") || currentScene.Equals("Rules"))
        {
            return;
        }

        if (isVisible)
        {
            HideEscMenu();
        }
        else
        {
            ShowEscMenu();
        }
    }

    private void HideEscMenu()
    {
        Time.timeScale = 1f;
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        isVisible = false;
    }

    private void ShowEscMenu()
    {
        Time.timeScale = 0f;
        canvasGroup.alpha += 1f;
        canvasGroup.blocksRaycasts = true;
        isVisible = true;
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
        if (EscClick != null)
        {
            EscClick.performed -= ToggleEscMenu;
        }
        AudioManager.OnAudioManagerMusicToggle -= OnAudioManagerMusicToggle;
        AudioManager.OnAudioManagerSoundToggle -= OnAudioManagerSoundToggle;
    }
}
