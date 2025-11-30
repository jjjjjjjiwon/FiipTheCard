using UnityEngine;
using UnityEngine.UI;

public class EnemyGroggyUI : MonoBehaviour
{
    [Header("UI")]
    public Slider GroggySlider;
    
    [Header("Target")]
    public EnemyGroggy enemyGroggy;
    
    void Start()
    {
        // EnemyGroggy 자동 찾기
        if (enemyGroggy == null)
        {   
            EnemyEntity player = FindObjectOfType<EnemyEntity>();
            if (player != null)
                enemyGroggy = player.Groggy;
        }
        
        // Slider 초기화
        if (GroggySlider != null && enemyGroggy != null)
        {
            GroggySlider.maxValue = enemyGroggy.groggyDuration;
            Debug.Log($"Grrrrrrogggggy");

            GroggySlider.value = enemyGroggy.GroggyPercent;
        }
    }
    
    void Update()
    {
        if (GroggySlider != null && enemyGroggy != null)
        {
            GroggySlider.value = enemyGroggy.GroggyPercent;
        }
    }
}
