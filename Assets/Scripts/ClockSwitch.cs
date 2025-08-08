using System;
using UnityEngine;
using UnityEngine.Audio;

public class ClockSwitch : MonoBehaviour
{
    [SerializeField] private AudioClip clockSound;

    private AudioSource audioSource;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private Color[] colors = { new Color(0.9294118f, 0.1098039f, 0.1411765f), new Color(0.1333333f, 0.6941177f, 0.2980392f), new Color(0f, 0.6352941f, 0.9058824f) };

    public static event Action<string> OnClockExpired;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        PlayerController.OnClockCollected += OnClockCollected;
    }

    private void OnClockCollected(Vector3 position, string color)
    {
        if (transform.position.Equals(position))
        {
            col.enabled = false;
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = 0f;
            spriteRenderer.color = spriteColor;

            audioSource.PlayOneShot(clockSound);

            Invoke(nameof(ResetClock), 5f);
        }
    }

    private void ResetClock()
    {
        col.enabled = true;
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 1f;
        spriteRenderer.color = spriteColor;

        OnClockExpired?.Invoke(spriteRenderer.name);
    }

    private void OnDisable()
    {
        PlayerController.OnClockCollected -= OnClockCollected;
    }
}
