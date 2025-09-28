using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

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

    void FixedUpdate()
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
}
