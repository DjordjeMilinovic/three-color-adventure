using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static event Action<Vector3, string> OnColorSwtiched;
    public static event Action<Vector3, string> OnClockCollected;
    public static event Action OnGoalReached;

    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private Color redColor;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color blueColor;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool goalReached;

    private InputAction moveAction;
    private InputAction restartAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        goalReached = false;

        moveAction = InputSystem.actions.FindAction("Move");
        restartAction = InputSystem.actions.FindAction("Restart");
        restartAction.performed += RestartLevel;
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
        if (collision.CompareTag("ColorSwitch"))
        {
            string color = "";
            switch (collision.name)
            {
                case "ColorRed":
                    color = "Red";
                    break;
                case "ColorGreen":
                    color = "Green";
                    break;
                case "ColorBlue":
                    color = "Blue";
                    break;
            }
            OnColorSwtiched?.Invoke(collision.transform.position, color);
            ChangePlayerColor(color);
        }
        if (collision.CompareTag("ClockSwitch"))
        {
            string color = "";
            switch (collision.name)
            {
                case "ClockRed":
                    color = "Red";
                    break;
                case "ClockGreen":
                    color = "Green";
                    break;
                case "ClockBlue":
                    color = "Blue";
                    break;
            }
            if (WallManager.Instance.currentColor.Equals(color))
            {
                return;
            }
            OnClockCollected?.Invoke(collision.transform.position, color);
        }
    }

    private void ChangePlayerColor(string color)
    {
        switch (color)
        {
            case "Red":
                spriteRenderer.color = redColor;
                break;
            case "Green":
                spriteRenderer.color = greenColor;
                break;
            case "Blue":
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

    private void RestartLevel(InputAction.CallbackContext context)
    {
        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        levelManager.SetNextScene(SceneManager.GetActiveScene().name);
        levelManager.LoadNextScene();
    }

    private void OnDisable()
    {
        restartAction.performed -= RestartLevel;
    }
}
