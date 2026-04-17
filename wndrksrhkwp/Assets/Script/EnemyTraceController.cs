using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float raycastDistance = 0.5f; // 너무 짧으면 벽에 박힌 후 감지를 못할 수 있음
    public float traceDistance = 5f;
    public LayerMask obstacleLayer; // 장애물 레이어를 따로 지정하는 것이 성능에 좋습니다.

    private Transform player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // 적이 부딪혀서 빙글빙글 도는 것 방지
    }

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    private void FixedUpdate() // 물리 이동은 FixedUpdate 권장
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position);
        float dist = direction.magnitude;

        if (dist > traceDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 moveDir = direction.normalized;

        // 1. 장애물이 있는지 단 한 번만 체크합니다.
        // RaycastAll 대신 단일 Raycast를 사용하거나, LayerMask를 활용하세요.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, raycastDistance, obstacleLayer);
        Debug.DrawRay(transform.position, moveDir * raycastDistance, Color.red);

        // 2. 장애물이 감지되었다면 방향을 틉니다.
        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            // 옆으로 피해가는 방향 계산
            moveDir = Quaternion.Euler(0, 0, -90f) * moveDir;
        }

        // 3. 결정된 방향으로 단 한 번만 이동합니다.
        rb.linearVelocity = moveDir * moveSpeed;
    }
}
