using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShadowEnemy : MonoBehaviour
{
    [Header("설정")]
    public Transform player;          // 플레이어 트랜스폼
    public float startDelay = 3f;     // 스테이지 시작 후 나타날 때까지 대기 시간
    public float followDelay = 2f;    // 플레이어의 몇 초 전 기록을 따라갈 것인지

    private Queue<Vector3> positionHistory = new Queue<Vector3>(); // 위치 기록 저장소
    private bool isFollowing = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // 시작할 때는 안 보이게 설정
        spriteRenderer.enabled = false;
        col.enabled = false;
    }

    void Start()
    {
        // 지정된 시간 뒤에 출현
        StartCoroutine(AppearAfterDelay());
    }

    IEnumerator AppearAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);

        spriteRenderer.enabled = true;
        col.enabled = true;
        isFollowing = true;

        // 적의 시작 위치를 플레이어의 현재 위치로 초기화 (갑자기 튀어나오지 않게)
        transform.position = player.position;
    }

    void FixedUpdate() // 물리 이동은 FixedUpdate가 더 안정적입니다.
    {
        if (player == null) return;

        // 1. 플레이어의 현재 위치를 계속 기록
        positionHistory.Enqueue(player.position);

        // 2. 추적 시작 전까지는 기록만 쌓음
        if (!isFollowing) return;

        // 3. 기록된 데이터의 개수가 지연 시간(FixedUpdate 기준)만큼 쌓였을 때만 꺼내기
        // FixedUpdate는 초당 보통 50번 실행되므로 (지연시간 * 50)개 이상의 데이터가 쌓여야 함
        float delayFrames = followDelay / Time.fixedDeltaTime;

        if (positionHistory.Count > delayFrames)
        {
            // 가장 오래된 기록을 꺼내서 적의 위치로 지정
            transform.position = positionHistory.Dequeue();
        }
    }
}
