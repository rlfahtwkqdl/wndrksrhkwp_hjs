using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    [Header("속도 아이템 설정")]
    // 이 두 줄이 인스펙터에 '입력 칸'을 만들어줍니다.
    public float speedMultiplier = 2f; // 속도 배율 (직접 수정 가능)
    public float duration = 5f;        // 지속 시간 (직접 수정 가능)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                // 인스펙터에서 수정한 '그 값'을 플레이어에게 그대로 전달합니다.
                player.StartSpeedBoost(speedMultiplier, duration);
                Destroy(gameObject);
            }
        }
    }
}
