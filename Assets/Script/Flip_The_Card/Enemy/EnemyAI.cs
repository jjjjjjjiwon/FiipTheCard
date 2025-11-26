using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    public Transform target;  // 플레이어
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float attackRange = 2f;  // 공격 사거리
    
    private Rigidbody rb;
    private EnemyEntity enemyEntity;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemyEntity = GetComponent<EnemyEntity>();
    }
    
    void Start()
    {
        // 플레이어 자동으로 찾기
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }
    
    void Update()
    {
        if (target == null) return;
        
        // 그로기 중이면 추적 안 함
        if (enemyEntity.Groggy != null && enemyEntity.Groggy.IsGroggy)
            return;
        
        ChasePlayer();
    }
    
    void ChasePlayer()
    {
        // 플레이어와의 거리
        float distance = Vector3.Distance(transform.position, target.position);
        
        // 공격 사거리 밖이면 추적
        if (distance > attackRange)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;  // 수평 이동만
            
            // 이동
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            
            // 플레이어 방향으로 회전
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 0.1f);
            }
        }
    }
}
