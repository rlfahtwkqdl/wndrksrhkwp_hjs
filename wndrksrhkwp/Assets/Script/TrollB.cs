using UnityEngine;

public class TrollB : MonoBehaviour
{
    private SpriteRenderer sr;
    private Collider2D col;

    private bool isRevealed = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // 1. 처음엔 안 보이게
        sr.enabled = false;

        // 2. 처음엔 통과 가능하게(트리거)
        // **중요**: 인스펙터에서 BoxCollider2D의 Is Trigger가 켜져 있어야 합니다.
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 이미 발견됐거나 플레이어가 아니면 무시
        if (isRevealed || !collision.CompareTag("Player")) return;

        // 플레이어의 Rigidbody를 가져옵니다. (점프 속도를 확인하기 위해)
        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
        if (playerRb == null) return;

        // --- 핵심 로직: 머리 충돌 감지 ---

        // 1. 플레이어의 위치가 블록의 중심보다 아래에 있는지 확인
        bool isBelow = collision.transform.position.y < transform.position.y;

        // 2. 플레이어가 위로 점프 중인지 확인 (Y축 속도가 양수)
        // 아주 살짝만 올라가고 있어도 감지하기 위해 0.1f 정도 여유를 둡니다.
        bool isJumpingUp = playerRb.linearVelocity.y > 0.1f;

        // 두 조건이 모두 참일 때만 '머리 충돌'로 간주
        if (isBelow && isJumpingUp)
        {
            Reveal();
        }
    }

    private void Reveal()
    {
        isRevealed = true;
        sr.enabled = true; // 모습 보이기

        // 이제 밟을 수 있게 트리거 끄기 (단단해짐)
        // 유니티 시스템에 의해 즉시 물리 실체화됩니다.
        col.isTrigger = false;

       
    }
}
