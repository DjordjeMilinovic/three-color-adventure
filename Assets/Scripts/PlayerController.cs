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

    private InputAction moveAction;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool goalReached;

    public static event Action<Vector3, string> OnColorSwtiched;
    public static event Action OnGoalReached;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        goalReached = false;

        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void FixedUpdate()
    {
        if (goalReached)
        {
            return;
        }
        Vector2 movement = moveAction.ReadValue<Vector2>();
        UpdateAnimator(movement.x, movement.y);
        FlipPlayer(movement.x);
        rb.linearVelocity = movement * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            GoalReached();
            return;
        }
        OnColorSwtiched?.Invoke(collision.transform.position, collision.name);
        ChangePlayerColor(collision.name);
    }

    private void ChangePlayerColor(string color)
    {
        switch (color)
        {
            case "ColorRed":
                spriteRenderer.color = redColor;
                break;
            case "ColorGreen":
                spriteRenderer.color = greenColor;
                break;
            case "ColorBlue":
                spriteRenderer.color = blueColor;
                break;
        }
    }

    private void UpdateAnimator(float x, float y)
    {
        animator.SetFloat("x", Mathf.Abs(x));
        animator.SetFloat("y", y);
    }

    private void FlipPlayer(float x)
    {
        if (x < -0.01)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void GoalReached()
    {
        goalReached = true;
        spriteRenderer.color = Color.white;
        UpdateAnimator(0f, 0f);
        rb.linearVelocity = Vector2.zero;
        OnGoalReached?.Invoke();
    }

}
