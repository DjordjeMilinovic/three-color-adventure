using System;
using UnityEngine;

public class ColorSwitchController : MonoBehaviour
{

    [SerializeField] private AudioClip splashSoundClip;

    private AudioSource audioSource;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerController.OnColorSwtiched += OnColorSwitched;
    }

    private void OnColorSwitched(Vector3 position, string color)
    {
        if (transform.position.Equals(position))
        {
            audioSource.PlayOneShot(splashSoundClip);
            col.enabled = false;
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = 0f;
            spriteRenderer.color = spriteColor;
        }
        else
        {
            col.enabled = true;
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = 1f;
            spriteRenderer.color = spriteColor;
        }
    }

    private void OnDisable()
    {
        PlayerController.OnColorSwtiched -= OnColorSwitched;
    }
}
