using System;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
        public static event Action OnKeyCollected;

    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            col.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            OnKeyCollected?.Invoke();
        }
    }
}
