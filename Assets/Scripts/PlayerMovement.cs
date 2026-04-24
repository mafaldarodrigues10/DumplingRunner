using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public DumplingVisualController visual;

    public float gravityMultiplier = 1.5f;
    public float fallMultiplier = 2f;

    public float forwardSpeed = 10f;
    public float baseSpeed = 5f;
    public float speedIncreasePerStep = 5f;
    public float maxSpeed = 50f;
    public int scoreStep = 500;

    public float laneSpeed = 14f;
    public float laneDistance = 2.5f;
    public float jumpForce = 7f;

    public float runFrameTime = 0.35f;
    public float slideDuration = 0.9f;

    private Rigidbody rb;
    private int targetLane = 1;

    private bool isSliding = false;
    private float runAnimTimer = 0f;
    private bool runLeft = true;

    private CapsuleCollider playerCollider;

    private float normalHeight;
    private Vector3 normalCenter;

    public float slideColliderHeight = 0.45f;
    public Vector3 slideColliderCenter = new Vector3(0, 0.225f, 0);


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        forwardSpeed = baseSpeed;

        playerCollider = GetComponent<CapsuleCollider>();

        if (playerCollider != null)
        {
            normalHeight = playerCollider.height;
            normalCenter = playerCollider.center;
        }
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver)
            return;

        UpdateSpeed();

        rb.MovePosition(rb.position + Vector3.forward * forwardSpeed * Time.deltaTime);

        HandleLaneSwitch();
        HandleJump();
        HandleSlide();
        HandleRunVisual();

        if (transform.position.y < 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

            if (rb != null)
                rb.linearVelocity = Vector3.zero;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.65f);
    }

    void UpdateSpeed()
    {
        if (ScoreManager.instance == null) return;

        int steps = ScoreManager.instance.score / scoreStep;
        forwardSpeed = Mathf.Min(baseSpeed + steps * speedIncreasePerStep, maxSpeed);
    }

    void HandleLaneSwitch()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            targetLane = Mathf.Max(0, targetLane - 1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            targetLane = Mathf.Min(2, targetLane + 1);

        float targetX = (targetLane - 1) * laneDistance;
        float newX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * laneSpeed);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void HandleJump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded() && !isSliding)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);

            if (visual != null)
                visual.Jump();
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime;
        }
    }

    void HandleSlide()
    {
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && IsGrounded() && !isSliding)
        {
            isSliding = true;

            if (visual != null)
                visual.Slide(true);

            CancelInvoke(nameof(StopSlide));
            Invoke(nameof(StopSlide), slideDuration);
        }

        if (playerCollider != null)
        {
            playerCollider.height = slideColliderHeight;
            playerCollider.center = slideColliderCenter;
        }
    }

    void StopSlide()
    {
        isSliding = false;

        if (visual != null)
            visual.Slide(false);
        
        if (playerCollider != null)
        {
            playerCollider.height = normalHeight;
            playerCollider.center = normalCenter;
        }
    }

    void HandleRunVisual()
    {
        if (visual == null) return;
        if (!IsGrounded()) return;
        if (isSliding) return;

        runAnimTimer += Time.deltaTime;

        if (runAnimTimer >= runFrameTime)
        {
            runAnimTimer = 0f;
            runLeft = !runLeft;

            if (runLeft)
                visual.RunLeft();
            else
                visual.RunRight();
        }
    }
}