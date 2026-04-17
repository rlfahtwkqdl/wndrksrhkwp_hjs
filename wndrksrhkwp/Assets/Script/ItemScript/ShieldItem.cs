using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    [Header("쉴드 설정")]
    public float duration = 5f; // 이 쉴드의 무적 시간

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 부딪혔을 때
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                // 플레이어에게 무적 명령을 내리고, 설정된 시간을 전달함
                player.StartShield(duration);
                Destroy(gameObject); // 아이템 제거
            }
        }
    }
}
