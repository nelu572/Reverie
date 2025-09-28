using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;

    public float jumpForce = 10;
    public float maxSpeed = 10;
    public float acceleration = 1;
    float speed = 0;
    float prev_speed = 0;
    bool onGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, transform.localScale.y / 2);
        Debug.DrawRay(rb.position, Vector2.down * 1, Color.red);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
    }
    void FixedUpdate()
    {
        Move();
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }
    void Move()
    {
        int dir = 0;
        if (Input.GetKey(KeyCode.A)) dir -= 1;
        if (Input.GetKey(KeyCode.D)) dir += 1;

        prev_speed = rb.linearVelocity.x;
        if (dir == 0)
        {
            if (speed != 0)
            {
                speed = Mathf.MoveTowards(speed, 0, acceleration);
            }
        }
        else
        {
            speed = prev_speed + dir * acceleration;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
        }
        Debug.Log(speed);
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }
    void Jump()
    {
        OnGround_check();

    }
    void OnGround_check()
    {

    }
}
