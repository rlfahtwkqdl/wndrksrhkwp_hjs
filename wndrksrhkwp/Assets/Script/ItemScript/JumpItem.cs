using UnityEngine;

public class JumpItem : MonoBehaviour
{
    [Header("점프 아이템 설정")]
    public float jumpMultiplier = 1.5f; // 점프력 배율 (예: 1.5배)
    public float duration = 5f;         // 지속 시간

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                // 플레이어에게 점프력 증가 명령 전달
                player.StartJumpBoost(jumpMultiplier, duration);
                Destroy(gameObject);
            }
        }
    }
}
