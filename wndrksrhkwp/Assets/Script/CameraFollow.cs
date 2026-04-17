using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -10);

    [Header("Base Follow Speed")]
    public float smoothTime = 0.2f; // 기본 카메라 추적 부드러움
    private Vector3 currentVelocity = Vector3.zero;

    [Header("Look Ahead (Auto Side)")]
    public float lookAheadDistance = 3f;
    public float lookAheadSpeed = 2f; // 자동 앞지르기 전용 속도
    private float currentLookAhead;

    [Header("Manual Peek (W, S)")]
    public float peekUpDistance = 4f;
    public float peekDownDistance = 7f;
    public float peekSpeed = 3f;      // 수동 상하 둘러보기 전용 속도
    private float currentVerticalPeek;

    private void LateUpdate()
    {
        if (target == null) return;

        // 1. [자동] 옆으로 앞지르기 로직 (따로 계산)
        float direction = (target.localScale.x < 0) ? 1f : -1f;
        float targetLookAhead = direction * lookAheadDistance;
        // MoveTowards나 Lerp를 사용하여 수동 입력과 별개의 속도로 흐르게 함
        currentLookAhead = Mathf.Lerp(currentLookAhead, targetLookAhead, Time.deltaTime * lookAheadSpeed);

        // 2. [수동] W, S 상하 둘러보기 로직 (따로 계산)
        float targetVerticalPeek = 0f;
        var keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) targetVerticalPeek = peekUpDistance;
            else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) targetVerticalPeek = -peekDownDistance;
        }
        // 상하 이동만 담당하는 부드러운 보간
        currentVerticalPeek = Mathf.Lerp(currentVerticalPeek, targetVerticalPeek, Time.deltaTime * peekSpeed);

        // 3. [최종] 모든 옵셋을 합산하여 목표 위치 설정
        // 자동 앞지르기(X)와 수동 둘러보기(Y)를 각각 더해줍니다.
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x + currentLookAhead,
            target.position.y + offset.y + currentVerticalPeek,
            target.position.z + offset.z
        );

        // 4. 카메라 본체 이동
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);
    }
}