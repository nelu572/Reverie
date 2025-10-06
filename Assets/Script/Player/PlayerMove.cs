
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject GM;

    private Rigidbody2D rb;

    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float acceleration = 1;
    private float speed = 0;
    private float prev_speed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        prev_speed = rb.linearVelocity.x;
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);
        if (!(left ^ right))
        {
            if (speed != 0)
            {
                speed = Mathf.MoveTowards(speed, 0, acceleration * 2);
            }
        }
        else
        {
            int dir = 0;
            if (left) dir = -1;
            else dir = 1;
            speed = prev_speed + dir * acceleration;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
        }
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }
    private void Jump()
    {
        bool jumpKey = Input.GetKeyDown(KeyCode.Space);
        bool onGround = OnGround_check();
        if (onGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    private bool OnGround_check()
    {
        int layerMask = LayerMask.GetMask("Ground", "Wall");
        float RayLength = transform.localScale.y + 0.0125f;

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
