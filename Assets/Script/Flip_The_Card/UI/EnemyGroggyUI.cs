using UnityEngine;
using UnityEngine.UI;

public class EnemyGroggyUI : MonoBehaviour
{
    [Header("UI")]
    public Slider groggySlider;
    
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
        if (groggySlider != null)
        {
            groggySlider.minValue = 0f;
            groggySlider.maxValue = 1f;
            groggySlider.value = 1f;
            //Debug.Log($"Grrrrrrogggggy");
        }
    }
    
    void Update()
    {
        if (groggySlider != null && enemyGroggy != null)
        {
            if(enemyGroggy.IsGroggy)
            {
                groggySlider.value = enemyGroggy.GroggyPercent; // 1 → 0
            }
            else
            {
                groggySlider.value = 1f;  // ← 그로기 아니면 다시 최대치
            }
        }
    }
}
