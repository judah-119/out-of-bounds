using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class player_script : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 8f;

    [Header("jump")]
    [SerializeField] float jumppower = 16f;
    [SerializeField] float variableJumpMultiplier = 0.5f;
    [SerializeField] float fallGravityMultiplier = 1.5f;

    [Header("tilt")]
    [SerializeField] float maxTiltAngle = 10f;
    [SerializeField] float tiltSpeed = 10f;

    [Header("jump squash & stretch")]
    [SerializeField] float jumpSquashX = 1.2f;
    [SerializeField] float jumpSquashY = 0.8f;
    [SerializeField] float jumpStretchX = 0.8f;
    [SerializeField] float jumpStretchY = 1.2f;
    [SerializeField] float scaleLerpSpeed = 18f;

    [Header("ground check")]
    [SerializeField] LayerMask groundlayer;
    [SerializeField] Transform groundcheck;

    [Header("timing")]
    [SerializeField] float coyoteTime = 0.15f;
    [SerializeField] float jumpBufferTime = 0.15f;

    [Header("state")]
    [SerializeField] SpriteRenderer spriteRenderer;

    private float horizontal;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private bool jumpHeld;

    private Vector3 normalScale;
    private Vector3 targetScale;

    public static player_script Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        normalScale = transform.localScale;
        targetScale = normalScale;
    }

    void Update()
    {
        HandleCoyoteTime();
        HandleJumpBuffer();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleVariableJumpHeight();
        HandleBetterFalling();
        TryJump();
        HandleTilt();
        HandleStretchWhileRising();
        SmoothScaleReturn();
    }

    //=========================================================
    // Movement
    //=========================================================

    void HandleMovement()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (horizontal < 0)
            spriteRenderer.flipX = true;
        else if (horizontal > 0)
            spriteRenderer.flipX = false;
    }

    //=========================================================
    // Tilt
    //=========================================================

    void HandleTilt()
    {
        float targetAngle = isgrounded() ? -horizontal * maxTiltAngle : 0f;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            tiltSpeed * Time.fixedDeltaTime
        );
    }

    //=========================================================
    // Jump Logic
    //=========================================================

    void TryJump()
    {
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            // INSTANT SQUASH when jumping
            transform.localScale = new Vector3(jumpSquashX, jumpSquashY, 1f);

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumppower);

            jumpBufferCounter = 0;
            coyoteCounter = 0;
        }
    }

    void HandleStretchWhileRising()
    {
        if (!isgrounded() && rb.linearVelocity.y > 0.1f)
        {
            targetScale = new Vector3(jumpStretchX, jumpStretchY, 1f);
        }
        else
        {
            targetScale = normalScale;
        }
    }

    void SmoothScaleReturn()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            scaleLerpSpeed * Time.fixedDeltaTime
        );
    }

    void HandleVariableJumpHeight()
    {
        if (!jumpHeld && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y * variableJumpMultiplier
            );
        }
    }

    void HandleBetterFalling()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y *
                (fallGravityMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }

    //=========================================================
    // Timers
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
    // Input
    //=========================================================

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpBufferCounter = jumpBufferTime;
            jumpHeld = true;
        }

        if (context.performed)
            jumpHeld = true;

        if (context.canceled)
            jumpHeld = false;
    }

    public void attackpressed(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("attack pressed");
    }

    public void restart(InputAction.CallbackContext context)
    {
        if (context.started)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //=========================================================
    // Ground Check
    //=========================================================

    bool isgrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundlayer);
    }
}