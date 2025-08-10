using System;
using System.Drawing;
using UnityEngine;

public class ClockWipeController : MonoBehaviour
{
    public static event Action<Vector3, string> OnClockExpired;

    [SerializeField] private string color;
    [SerializeField] private Animator animator;
    private Vector3 position;

    private void Start()
    {
        PlayerController.OnClockCollected += OnClockCollected;
        PlayerController.OnColorSwtiched += OnColorSwtiched;
    }

    private void OnColorSwtiched(Vector3 position, string color)
    {
        if (!color.Equals(this.color))
        {
            return;
        }
        animator.Play("NoWipe");
        OnClockExpired?.Invoke(this.position, color);
    }

    private void OnClockCollected(Vector3 position, string color)
    {
        if (!color.Equals(this.color))
        {
            return;
        }
        this.position = position;
        animator.SetTrigger("StartWipe");
    }

    // Clock wipe animation event
    private void ClockExpired()
    {
        OnClockExpired?.Invoke(position, color);
    }


    private void OnDisable()
    {
        PlayerController.OnClockCollected -= OnClockCollected;
        PlayerController.OnColorSwtiched -= OnColorSwtiched;
    }
}
