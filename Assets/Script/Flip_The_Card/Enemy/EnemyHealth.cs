using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
     [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;
    
    [Header("Groggy")]
    public float groggyThreshold = 30f;  // 이만큼 데미지 받으면 그로기
    private float accumulatedDamage = 0f;
    private bool isGroggy = false;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        accumulatedDamage += damage;
        
        Debug.Log($"Boss took {damage} damage! HP: {currentHealth}/{maxHealth}");
        
        // 그로기 체크
        if (!isGroggy && accumulatedDamage >= groggyThreshold)
        {
            EnterGroggy();
        }
        
        // 사망 체크
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void EnterGroggy()
    {
        isGroggy = true;
        accumulatedDamage = 0f;
        Debug.Log(">>> BOSS GROGGY!");
        
        // 일정 시간 후 그로기 해제 (나중에 피니쉬로 해제)
        Invoke(nameof(ExitGroggy), 5f);
    }
    
    public void ExitGroggy()
    {
        isGroggy = false;
        Debug.Log("Boss recovered from groggy");
    }
    
    void Die()
    {
        Debug.Log(">>> BOSS DEFEATED!");
        // 보스 사망 처리
        gameObject.SetActive(false);
    }
    
    public bool IsGroggy => isGroggy;
}
