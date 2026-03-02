using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class player_script : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] Rigidbody2D Rigidbody2D;

    [Header("stats")]
    [SerializeField] float speed;
    [SerializeField] float jumppower;
    [SerializeField] float variableJumpMultiplier = 0.5f;
    [SerializeField] float fallGravityMultiplier = 1.5f;

    [Header("ground check")]
    [SerializeField] LayerMask groundlayer;
    [SerializeField] Transform groundcheck;

    [Header("timing")]
    [SerializeField] float coyoteTime = 0.15f;   // NEW
    [SerializeField] float jumpBufferTime = 0.15f; // NEW

    [Header("state")]
    [SerializeField] float Horizontal;
    [SerializeField] SpriteRenderer spriteRenderer;

    // internal timers
    private float coyoteCounter;
    private float jumpBufferCounter;

    private bool jumpHeld;
    public static player_script Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);

        }
        else
        {
            Instance = this;
        }

    }

    void Update()
    {
        HandleCoyoteTime();
        HandleJumpBuffer();
    }

    void FixedUpdate()
    {
        // MOVEMENT
        Rigidbody2D.linearVelocity = new Vector2(Horizontal * speed, Rigidbody2D.linearVelocity.y);

        // WALK ANIMATION + SPRITE FLIP
        if (Horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (Horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        // VARIABLE JUMP HEIGHT + FALL GRAVITY
        HandleVariableJumpHeight();
        HandleBetterFalling();

        // TRY JUMPING (Buffer + Coyote)
        TryJump();
    }

    //=========================================================
    //                          TIMERS
    //=========================================================

    void HandleCoyoteTime()
    {
        if (isgrounded())
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;
    }

    void HandleJumpBuffer()
    {
        if (jumpBufferCounter > 0)
            jumpBufferCounter -= Time.deltaTime;
    }

    //=========================================================
    //                        JUMP LOGIC
    //=========================================================

    void TryJump()
    {
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            // Perform jump
            Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocity.x, jumppower);

            // Reset timers after jump
            jumpBufferCounter = 0;
            coyoteCounter = 0;
        }
    }

    void HandleVariableJumpHeight()
    {
        // Shorten jump when button released
        if (!jumpHeld && Rigidbody2D.linearVelocity.y > 0)
        {
            Rigidbody2D.linearVelocity = new Vector2(
                Rigidbody2D.linearVelocity.x,
                Rigidbody2D.linearVelocity.y * variableJumpMultiplier
            );
        }
    }

    void HandleBetterFalling()
    {
        if (Rigidbody2D.linearVelocity.y < 0)
        {
            Rigidbody2D.linearVelocity += Vector2.up * Physics2D.gravity.y *
                (fallGravityMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }

    //=========================================================
    //                  INPUT SYSTEM ACTIONS
    //=========================================================

    private bool isgrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundlayer);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Horizontal = context.ReadValue<Vector2>().x;
    }

    public void attackpressed(InputAction.CallbackContext context)
    {
        Debug.Log("attack pressed");
    }
    public void Jump(InputAction.CallbackContext context)
    {
        // Jump pressed
        if (context.started)
        {
            jumpBufferCounter = jumpBufferTime;
            jumpHeld = true;
        }

        // Jump held
        if (context.performed)
            jumpHeld = true;

        // Jump released
        if (context.canceled)
        {
            jumpHeld = false;
        }
    }
    public void restart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
