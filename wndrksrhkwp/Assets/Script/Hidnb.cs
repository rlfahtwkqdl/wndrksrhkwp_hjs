using UnityEngine;
using UnityEngine.Tilemaps;

public class Hidnb : MonoBehaviour
{
    private TilemapRenderer tr;
    private TilemapCollider2D tc;

    private void Awake()
    {
        tr = GetComponent<TilemapRenderer>();
        tc = GetComponent<TilemapCollider2D>();

        // 1. 처음엔 안 보이게
        tr.enabled = false;

        // 2. 처음엔 통과되면서 감지만 할 수 있게 '트리거'로 설정
        if (tc != null) tc.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 3. 모습 보이기
            tr.enabled = true;

            // 4. 핵심: 트리거를 끕니다! 이제부터는 통과 못 하는 '단단한 벽'이 됩니다.
            if (tc != null) tc.isTrigger = false;

            
        }
    }
}