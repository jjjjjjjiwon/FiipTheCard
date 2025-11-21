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
    private Vector3 dashDirection;        // 대쉬 방향 고정
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
        if (isDashing)
        {
            if (dashTime > 0)
            {
                // 고정된 방향으로 대시
                rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
                dashTime -= Time.fixedDeltaTime;
            }
            else
            {
                EndDash();
            }
        }
    }
    
    // 대시 시작
    void StartDash()
    {
        isDashing = true;

        // 입력이 있으면 입력 방향, 없으면 현재 보는 방향
        if (playerMovement.MoveInput.sqrMagnitude > 0.01f)
        {
            dashDirection = playerMovement.MoveInput;
        }
        else
        {
            dashDirection = transform.forward;
        }

        dashTime = dashDuration;
        cooltimeTime = dashCooltime;
    }

    // 대시 종료
    void EndDash()
    {
        isDashing = false;
        dashTime = 0f;  // 대시 타이머 초기화
    }
}
