using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;
    private float moveInput;
    private float originalSpeed; // 원래 속도를 저장할 변수
    private Coroutine speedCoroutine;
    private Coroutine shieldCoroutine;
    private float originalJumpForce;
    private Coroutine jumpCoroutine; // 점프 코루틴 관리 변수

    [Header("Jump Boost UI")]
    public GameObject jumpIcon; // 점프 아이콘 (선택 사항)

  


    [Header("Speed Boost UI")]
    public GameObject speedIcon; // 속도 아이콘 (선택 사항)

    [Header("Shield UI")]
    public GameObject shieldIcon; // 에디터에서 ShieldIcon 오브젝트를 여기에 드래그 앤 드롭
    private bool isInvincible = false;

    private bool canDoubleJump;

    float score;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
        score = 0f;

        originalSpeed = moveSpeed; // 게임 시작 시 설정된 기본 속도를 저장해둠
        originalSpeed = moveSpeed;
        originalJumpForce = jumpForce; // 기본 점프력을 저장해둠
    }

    private void Update()
    {
        // 1. 이동 속도 계산
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 2. 바닥 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // 3. 캐릭터 좌우 반전 (방향 전환)
        if (moveInput < 0)
            transform.localScale = new Vector3(0.31f, 0.31f, 1);
        else if (moveInput > 0)
            transform.localScale = new Vector3(-0.31f, 0.31f, 1);

        // 4. 애니메이션 업데이트
        // moveInput이 0이 아니면(움직이면) isWalking은 true, 아니면 false가 됩니다.
        pAni.SetBool("Walking", moveInput != 0 && isGrounded);

        // (선택 사항) 점프 중인지 여부도 넘겨주면 좋습니다.
        pAni.SetBool("isGrounded", isGrounded);
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            if (isGrounded)
            {
                // 첫 번째 점프 (바닥일 때)
                PerformJump();
                pAni.SetTrigger("Jump");
            }
            else if (canDoubleJump)
            {
                // 두 번째 점프 (공중이고 권한이 있을 때)
                PerformJump();
                canDoubleJump = false; // 점프 권한 소진!

                pAni.SetTrigger("Jump");
            }
        }
    }

    // 반복되는 점프 로직을 별도 메서드로 분리하면 깔끔합니다.
    private void PerformJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void StartJumpBoost(float multiplier, float duration)
    {
        if (jumpCoroutine != null)
            StopCoroutine(jumpCoroutine);

        jumpCoroutine = StartCoroutine(JumpBoostRoutine(multiplier, duration));
    }

    IEnumerator JumpBoostRoutine(float multiplier, float duration)
    {
        // 1. 점프력 증가
        jumpForce = originalJumpForce * multiplier;
        if (jumpIcon != null) jumpIcon.SetActive(true);

       

        // 2. 시간 대기
        yield return new WaitForSeconds(duration);

        // 3. 점프력 원복
        jumpForce = originalJumpForce;
        if (jumpIcon != null) jumpIcon.SetActive(false);

        
        jumpCoroutine = null;
    }

    public void StartSpeedBoost(float multiplier, float duration)
    {
        score += 100f;
        // 전체를 멈추지 말고, 속도 코루틴만 돌고 있다면 멈춤
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        speedCoroutine = StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        moveSpeed = originalSpeed * multiplier;
        if (speedIcon != null) speedIcon.SetActive(true);

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
        if (speedIcon != null) speedIcon.SetActive(false);

        speedCoroutine = null; // 종료 후 비워주기
    }

    public void StartShield(float duration)
    {
        score += 100f;
        // 중요: StopAllCoroutines()를 지우고, 실드 코루틴만 관리함
        if (shieldCoroutine != null)
            StopCoroutine(shieldCoroutine);

        shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
    }

    IEnumerator ShieldRoutine(float duration)
    {


        isInvincible = true;
        if (shieldIcon != null) shieldIcon.SetActive(true);

        yield return new WaitForSeconds(duration);

        if (shieldIcon != null) shieldIcon.SetActive(false);
        isInvincible = false;

        shieldCoroutine = null; // 종료 후 비워주기
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvincible)
        {
            // 닿은 대상이 적(Enemy)이라면
            if (collision.CompareTag("Enemy"))
            {
                // 적을 파괴합니다!
                Destroy(collision.gameObject);

                score += 100f;
            }
        }

        // 쉴드 아이템 자체에서 처리를 하므로 여기서는 체크할 필요가 없어집니다.
        // 다만 무적 판정은 유지합니다.
        if (!isInvincible)
        {
            if (collision.CompareTag("Respawn") || collision.CompareTag("Enemy"))
            {
                score += 1f;

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (collision.CompareTag("Finish"))
        {
            HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, (int)score);

            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }
    }

    
}