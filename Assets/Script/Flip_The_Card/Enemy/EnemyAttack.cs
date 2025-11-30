using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackCooldown = 2f;  // 공격 쿨타임
    public float attackRange = 2f;     // 공격 사거리
    
    [Header("Weapon")]
    public WeaponHitbox weaponHitbox;  // 보스 무기
    
    private float attackTimer = 0f;
    private Transform target;
    private EnemyEntity enemyEntity;
    
    void Awake()
    {
        enemyEntity = GetComponent<EnemyEntity>();
    }
    
    void Start()
    {
        // 플레이어 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
        
        // 무기 히트박스 자동으로 찾기
        if (weaponHitbox == null)
            weaponHitbox = GetComponentInChildren<WeaponHitbox>();
    }
    
    void Update()
    {
        if (target == null) return;
        
        // 그로기 중이면 공격 안 함
        if (enemyEntity.Groggy != null && enemyEntity.Groggy.IsGroggy)    
        return;
        
        // 쿨타임 감소
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        
        // 공격 가능하면 공격
        if (attackTimer <= 0)
        {
            TryAttack();
        }
    }
    
    void TryAttack()
    {
        // 플레이어와의 거리 체크
        float distance = Vector3.Distance(transform.position, target.position);
        
        if (distance <= attackRange)
        {
            ExecuteAttack();
            attackTimer = attackCooldown;  // 쿨타임 초기화
        }
    }
    
    void ExecuteAttack()
    {
        Debug.Log("Boss Attack!");
        
        // 히트박스 활성화
        if (weaponHitbox != null)
        {
            weaponHitbox.EnableHit();
            Invoke(nameof(DisableHitbox), 0.3f);  // 0.3초 후 비활성화
        }
    }
    
    void DisableHitbox()
    {
        if (weaponHitbox != null)
            weaponHitbox.DisableHit();
    }
}
