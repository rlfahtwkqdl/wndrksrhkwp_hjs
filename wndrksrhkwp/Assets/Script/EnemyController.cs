using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private bool isMovingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // 1. 적이 부딪혀서 넘어지지 않도록 Z축 회전 고정 (필수!)
        rb.freezeRotation = true;

        // 2. 타일 안으로 파고드는 걸 막기 위해 설정을 코드로 강제합니다.
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void FixedUpdate()
    {
        // 3. 이동 처리 (물리 기반으로 안정적으로 이동)
        float horizontalVelocity = isMovingRight ? moveSpeed : -moveSpeed;
        rb.linearVelocity = new Vector2(horizontalVelocity, rb.linearVelocity.y);

        // 4. 방향에 맞춰 스프라이트 반전
        FlipSprite();
    }

    private void FlipSprite()
    {
        // scale x값을 조절해 좌우 반전
        float xSize = Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(isMovingRight ? -xSize : xSize, transform.localScale.y, 1);
    }

    // Is Trigger가 체크된 Boundary 오브젝트와 닿았을 때 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            isMovingRight = !isMovingRight;
        }
    }
}
