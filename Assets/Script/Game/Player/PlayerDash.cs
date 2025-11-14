using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 30f;
    public float dashDuration = 0.2f;
    public float dashCooltime = 0.1f;
    public bool IsDashing => isDashing;


    private bool isDashing = false;       // 대시 상태
    private float dashTime = 0f;          // 대시 타이머
    private float cooltimeTime = 0f;      // 쿨타임 타이머
    private PlayerMovement playerMovement;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // 쿨타임 처리
        if (cooltimeTime > 0)
        {
            cooltimeTime -= Time.deltaTime;
        }

        // 대시 입력 받기
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooltimeTime <= 0 && !isDashing)
        {
            StartDash();  // 대시 시작
            Debug.Log("Dash started");
        }
    }

    void FixedUpdate()
    {
        // 대시 중이면, 대시 속도를 적용
        if (isDashing)
        {
            // 대시가 진행 중일 때만 이동
            if (dashTime > 0)
            {
                rb.MovePosition(rb.position + playerMovement.MoveInput * dashSpeed * Time.fixedDeltaTime);
                dashTime -= Time.fixedDeltaTime;  // 대시 타이머 감소
            }
            else
            {
                EndDash();  // 대시 종료
            }
        }
    }

    // 대시 시작
    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;  // 대시 지속 시간 초기화
        cooltimeTime = dashCooltime;  // 쿨타임 설정
    }

    // 대시 종료
    void EndDash()
    {
        isDashing = false;
        dashTime = 0f;  // 대시 타이머 초기화
    }
}
