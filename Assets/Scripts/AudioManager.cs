using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip colorSplashSound;
    [SerializeField] private AudioClip clockSound;
    [SerializeField] private AudioClip doorSound;

    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();

        ClockSwitch.OnClockExpired += OnClockExpired;

        PlayerController.OnClockCollected += OnClockCollected;
        PlayerController.OnColorSwtiched += OnColorSwitched;

        ButtonClick.OnButtonClicked += OnButtonClicked;

        KeyManager.OnKeyCollected += OnKeyCollected;
    }

    private void OnClockExpired(string obj)
    {
        audioSource.PlayOneShot(clockSound);
    }
    private void OnClockCollected(Vector3 vector, string color)
    {
        if (WallManager.Instance.currentColor.Equals(color))
        {
            return;
        }
        audioSource.PlayOneShot(clockSound);
    }
    private void OnColorSwitched(Vector3 vector, string arg2)
    {
        audioSource.PlayOneShot(colorSplashSound);
    }
    private void OnButtonClicked()
    {
        audioSource.PlayOneShot(buttonClick);
    }
    private void OnKeyCollected()
    {
        audioSource.PlayOneShot(doorSound);
    }

}
