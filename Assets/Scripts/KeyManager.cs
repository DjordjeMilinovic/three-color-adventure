using System;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private AudioClip doorSoundClip;

    private AudioSource audioSource;
    private Collider2D col;

    public static event Action OnKeyCollected;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnKeyCollected?.Invoke();
            audioSource.PlayOneShot(doorSoundClip);
            col.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
