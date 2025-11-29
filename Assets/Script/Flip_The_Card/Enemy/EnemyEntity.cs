using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [Header("Components")]
    private Health health;
    private EnemyAI ai;
    private EnemyAttack attack;
    private EnemyGroggy groggy;
    
    // 외부 접근용 프로퍼티
    public Health Health => health;
    public EnemyAI AI => ai;
    public EnemyAttack Attack => attack;
    //public EnemyGroggy Groggy => groggy;
    
    void Awake()
    {
        health = GetOrAddComponent<Health>();
        ai = GetOrAddComponent<EnemyAI>();
        attack = GetOrAddComponent<EnemyAttack>();
        //groggy = GetOrAddComponent<EnemyGroggy>();
    }
    
    T GetOrAddComponent<T>() where T : Component
    {
        T component = GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }
}
