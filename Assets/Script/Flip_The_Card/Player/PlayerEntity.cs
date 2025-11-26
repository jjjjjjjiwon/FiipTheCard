using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [Header("Components")]
    private Health health;
    private PlayerMovement movement;
    private PlayerDash dash;
    private PlayerCombat combat;
    
    // 외부 접근용 프로퍼티
    public Health Health => health;
    public PlayerMovement Movement => movement;
    public PlayerDash Dash => dash;
    public PlayerCombat Combat => combat;
    
    void Awake()
    {
        health = GetOrAddComponent<Health>();
        movement = GetOrAddComponent<PlayerMovement>();
        dash = GetOrAddComponent<PlayerDash>();
        combat = GetOrAddComponent<PlayerCombat>();
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