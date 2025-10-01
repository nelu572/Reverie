
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject GM;
    Inputs input;

    Rigidbody2D rb;

    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float acceleration = 1;
    float speed = 0;
    float prev_speed = 0;

    bool onGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GM.GetComponent<Inputs>();
    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        prev_speed = rb.linearVelocity.x;
        bool left = input.Get_Keys(KeyCode.A);
        bool right = input.Get_Keys(KeyCode.D);
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
    void Jump()
    {
        OnGround_check();

    }
    void OnGround_check()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(rb.position, Vector3.down, transform.localScale.y);
        Debug.DrawRay(rb.position, Vector2.down * 1, Color.red);
        
    }
}
