using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthUI : MonoBehaviour
{
    [Header("UI")]
    public Slider healthSlider;
    
    [Header("Target")]
    public Health EnemyHealth;
    
    void Start()
    {
        // 플레이어 Health 자동으로 찾기
        if (EnemyHealth == null)
        {   
            EnemyEntity player = FindObjectOfType<EnemyEntity>();
            if (player != null)
                EnemyHealth = player.Health;
        }
        
        // Slider 초기화
        if (healthSlider != null && EnemyHealth != null)
        {
            healthSlider.maxValue = EnemyHealth.maxHealth;
            Debug.Log($"Current HP: {EnemyHealth.CurrentHealth}");

            healthSlider.value = EnemyHealth.CurrentHealth;
        }
    }
    
    void Update()
    {
        if (healthSlider != null && EnemyHealth != null)
        {
            healthSlider.value = EnemyHealth.CurrentHealth;
        }
    }
}
