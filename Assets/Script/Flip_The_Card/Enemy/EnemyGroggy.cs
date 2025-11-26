using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroggy : MonoBehaviour
{
    [Header("Groggy Settings")]
    public float groggyThreshold = 30f;  // 그로기 진입 데미지
    public float groggyDuration = 5f;    // 그로기 지속 시간 (피니쉬 안 맞으면)
    
    private float accumulatedDamage = 0f;
    private bool isGroggy = false;
    private float groggyTimer = 0f;
    
    public bool IsGroggy => isGroggy;
    
    private EnemyEntity enemyEntity;
    
    void Awake()
    {
        enemyEntity = GetComponent<EnemyEntity>();
    }
    
    void Start()
    {
        // Health의 데미지 이벤트 연결 (간단 버전)
    }
    
    void Update()
    {
        if (isGroggy)
        {
            groggyTimer -= Time.deltaTime;
            
            if (groggyTimer <= 0)
            {
                ExitGroggy();
            }
        }
    }
    
    public void OnTakeDamage(float damage)
    {
        if (isGroggy) return;  // 그로기 중엔 누적 안 됨
        
        accumulatedDamage += damage;
        Debug.Log($"Boss accumulated damage: {accumulatedDamage}/{groggyThreshold}");
        
        if (accumulatedDamage >= groggyThreshold)
        {
            EnterGroggy();
        }
    }
    
    void EnterGroggy()
    {
        isGroggy = true;
        accumulatedDamage = 0f;
        groggyTimer = groggyDuration;
        
        Debug.Log(">>> BOSS GROGGY!");
    }
    
    public void ExitGroggy()
    {
        isGroggy = false;
        groggyTimer = 0f;
        
        Debug.Log("Boss recovered from groggy");
    }
    
    // 피니쉬 맞으면 즉시 해제
    public void ExitByFinisher()
    {
        Debug.Log("Boss groggy ended by finisher!");
        ExitGroggy();
    }
}
