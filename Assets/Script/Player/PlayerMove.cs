using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject GM;

    private Rigidbody2D rb;

    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float acceleration = 1;
    private float speed = 0;
    private float prevSpeed = 0;

    [SerializeField] private float jumpBufferTime = 0.1f;
    private float bufferTime;
    [SerializeField] private bool jumpKey;

    private bool leftKey;
    private bool rightKey;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        leftKey = Input.GetKey(KeyCode.A);
        rightKey = Input.GetKey(KeyCode.D);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKey = true;
            bufferTime = jumpBufferTime;
        }
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        prevSpeed = rb.linearVelocity.x;
        if (!(leftKey ^ rightKey))
        {
            if (speed != 0)
            {
                speed = Mathf.MoveTowards(speed, 0, acceleration * 2);
            }
        }
        else
        {
            int dir = 0;
            if (leftKey) dir = -1;
            else dir = 1;
            speed = prevSpeed + dir * acceleration;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
        }
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }
    private void Jump()
    {
        bool onGround = OnGround_check();
        if (jumpKey)
        {
            bufferTime -= Time.deltaTime;
            if (bufferTime <= 0)
            {
                jumpKey = false;
            }
            if (onGround)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpKey = false;
            }
        }
    }
    private bool OnGround_check()
    {
        int layerMask = LayerMask.GetMask("Ground", "Wall");
        float RayLength = transform.localScale.y + 0.00390625f;

        Vector2 rightRayPosition = new Vector2(rb.position.x + (transform.localScale.x / 2), rb.position.y);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRayPosition, Vector3.down, RayLength, layerMask);

        Vector2 leftRayPosition = new Vector2(rb.position.x - (transform.localScale.x / 2), rb.position.y);
        RaycastHit2D leftHit = Physics2D.Raycast(leftRayPosition, Vector3.down, RayLength, layerMask);

        Color rightColor = (rightHit.collider != null) ? Color.green : Color.red;
        Color leftColor = (leftHit.collider != null) ? Color.green : Color.red;

        Debug.DrawRay(rightRayPosition, Vector2.down * RayLength, rightColor);
        Debug.DrawRay(leftRayPosition, Vector2.down * RayLength, leftColor);

        return rightHit.collider != null || leftHit.collider != null;
    }
}
