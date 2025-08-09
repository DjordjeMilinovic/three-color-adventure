using System;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private Collider2D col;

    public static event Action OnKeyCollected;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnKeyCollected?.Invoke();
            col.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
