
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

    private bool onGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        bool jump = Input.GetKey(KeyCode.Space);
        if (jump)
        {
            Jump();
        }
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
        onGround = OnGround_check();
        // if (onGround)
        // {
        //     rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        // }
    }
    private bool OnGround_check()
    {
        int layerMask = LayerMask.GetMask("Ground", "Wall");

        // 캐릭터의 바닥 크기 설정 (콜라이더 크기를 참고하는 게 가장 정확)
        Vector2 boxSize = new Vector2(transform.localScale.x * 0.9f, 0.1f); // 가로 폭은 캐릭터, 세로는 얇게
        float castDistance = 0.1f;  // 캐릭터 발 바로 아래 체크할 거리

        // BoxCast (중심, 크기, 회전각, 방향, 거리, 레이어마스크)
        RaycastHit2D hit = Physics2D.BoxCast(rb.position, boxSize, 0f, Vector2.down, castDistance, layerMask);

        // Scene 뷰 디버그용 박스 그리기
        Debug.DrawRay(rb.position, Vector2.down * castDistance, Color.red); // 중심선
        Debug.DrawLine(rb.position + new Vector2(-boxSize.x / 2, 0), rb.position + new Vector2(-boxSize.x / 2, -castDistance), Color.green);
        Debug.DrawLine(rb.position + new Vector2(boxSize.x / 2, 0), rb.position + new Vector2(boxSize.x / 2, -castDistance), Color.green);

        return hit.collider != null;
    }
}
