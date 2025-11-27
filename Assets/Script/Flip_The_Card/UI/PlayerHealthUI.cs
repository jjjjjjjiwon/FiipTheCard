using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("UI")]
    public Slider healthSlider;
    
    [Header("Target")]
    public Health playerHealth;
    
    void Start()
    {
        // 플레이어 Health 자동으로 찾기
        if (playerHealth == null)
        {
            PlayerEntity player = FindObjectOfType<PlayerEntity>();
            if (player != null)
                playerHealth = player.Health;
        }
        
        // Slider 초기화
        if (healthSlider != null && playerHealth != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.CurrentHealth;
        }
    }
    
    void Update()
    {
        if (healthSlider != null && playerHealth != null)
        {
            healthSlider.value = playerHealth.CurrentHealth;
        }
    }
}