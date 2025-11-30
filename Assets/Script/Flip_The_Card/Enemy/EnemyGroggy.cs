using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroggy : MonoBehaviour
{
   [Header("Groggy Settings")]
    public float groggyDuration = 5f;   // 그로기 지속 시간
    public float defenseTime = 1f;      // 그로기 방어 시간
    
    private bool isGroggy = false;      // 그로기 여부
    private bool isDefense = false;     // 그로기 방어 여부
    private float groggyTimer = 0f;     // 그로기 시간
    
    public bool IsGroggy => isGroggy;
    public bool IsDefense => isDefense;
    public float GroggyPercent => groggyTimer / groggyDuration;
    
    void Update()
    {
        if (isGroggy)
        {
            groggyTimer -= Time.deltaTime;
            
            if (ShouldExitGroggy())
            {
                ExitGroggy();
            }
        }
    }

    bool ShouldExitGroggy()
    {
    // 기본: 타이머 끝남
    if (groggyTimer <= 0)
        return true;
    
    // 나중에 추가 가능:
    // if (특수공격맞음) return true;
    // if (특수조건) return true;
    return false;
    }
    
    public void EnterGroggy()
    {
        if (isDefense) return;
        if (isGroggy) return;
        
        isGroggy = true;
        groggyTimer = groggyDuration;
        Debug.Log(">>> BOSS GROGGY!");
    }
    
    public void ExitGroggy()
    {
        if (!isGroggy) return;
        
        isGroggy = false;
        groggyTimer = 0f;
        Debug.Log("Boss Groggy Exit");
        
        StartDefense();
    }
    
    public void ExitByFinisher()
    {
        if (!isGroggy) return;
        
        Debug.Log("Boss Groggy Exit by Finisher!");
        ExitGroggy();
    }
    
    void StartDefense()
    {
        isDefense = true;
        Debug.Log("Boss Defense Start");
        Invoke(nameof(EndDefense), defenseTime);
    }
    
    void EndDefense()
    {
        isDefense = false;
        Debug.Log("Boss Defense End");
    }

}
