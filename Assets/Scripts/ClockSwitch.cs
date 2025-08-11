using UnityEngine;

public class ClockSwitch : MonoBehaviour
{
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        PlayerController.OnClockCollected += OnClockCollected;
        ClockWipeController.OnClockExpired += OnClockExpired;
    }

    private void OnClockCollected(Vector3 position, string color)
    {
        if (WallManager.Instance.currentColor.Equals(color))
        {
            return;
        }

        if (transform.position.Equals(position))
        {
            HideClock();
        }
    }

    private void OnClockExpired(Vector3 position, string color)
    {
        if (!transform.position.Equals(position))
        {
            return;
        }
        ShowClock();
    }

    private void HideClock()
    {
        col.enabled = false;
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 0f;
        spriteRenderer.color = spriteColor;
    }

    private void ShowClock()
    {
        col.enabled = true;
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 1f;
        spriteRenderer.color = spriteColor;
    }

    private void OnDisable()
    {
        PlayerController.OnClockCollected -= OnClockCollected;
        ClockWipeController.OnClockExpired -= OnClockExpired;
    }
}
