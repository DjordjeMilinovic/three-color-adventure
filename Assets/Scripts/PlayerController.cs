using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private Color redColor;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color blueColor;

    InputAction moveAction;
    private Rigidbody2D rb;

    public static event Action<Vector3, string> OnColorSwtiched;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void FixedUpdate()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = movement * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "ColorRed":
                gameObject.GetComponent<SpriteRenderer>().color = redColor;
                OnColorSwtiched?.Invoke(collision.transform.position, "Red");
                break;
            case "ColorGreen":
                gameObject.GetComponent<SpriteRenderer>().color = greenColor;
                OnColorSwtiched?.Invoke(collision.transform.position, "Green");
                break;
            case "ColorBlue":
                gameObject.GetComponent<SpriteRenderer>().color = blueColor;
                OnColorSwtiched?.Invoke(collision.transform.position, "Blue");
                break;
        }

    }

}
