using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 10f;
    public LayerMask layerMask;
    public int maxJumpCount = 2;
    public float groundCheckDistance = 0.1f;

    private int jumpCount;
    private bool iGround;
    private bool iJumpCheck;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (iGround || jumpCount > 0))
        {
            Debug.Log("Space bar");
            Jump();
        }      
    }
    
    void FixedUpdate()
    {
        GroundCheck();
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount--;
    }

    void GroundCheck()
    {
        iGround = Physics.Raycast(transform.position - Vector3.up * 0.5f, Vector3.down, groundCheckDistance, layerMask);

        if (iGround)
        {
            jumpCount = maxJumpCount; // 바닥에 있을 때는 점프 횟수 리셋
        }
    }
}
