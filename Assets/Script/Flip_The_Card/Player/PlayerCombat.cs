using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<ComboData> comboDatas;
    public WeaponHitbox weaponHitbox;

    private List<KeyCode> currentCombo = new List<KeyCode>();

    void Start()
    {
        weaponHitbox = GetComponentInChildren<WeaponHitbox>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddComboInput(KeyCode.J);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            AddComboInput(KeyCode.K);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            AddComboInput(KeyCode.L);
        }
    }
    
    void AddComboInput(KeyCode key)
    {
        currentCombo.Add(key);
        Debug.Log($"Current Combo: {string.Join(" → ", currentCombo)}");
        
        // 모든 콤보 체크
        for (int i = 0; i < comboDatas.Count; i++)
        {
            if (MatchesCombo(comboDatas[i]))
            {
                Debug.Log($"✓ Valid! Matching: {comboDatas[i].comboName}");
                
                // 콤보 완성 체크
                if (currentCombo.Count == comboDatas[i].comboSequence.Count)
                {
                    Debug.Log($">>> COMBO COMPLETE: {comboDatas[i].comboName}");
                    ExecuteFinisher(comboDatas[i]);
                    return;
                }
                return; // 진행 중
            }

            if (MatchesCombo(comboDatas[i]))
            {
                ExecuteAttack();  // 공격 실행 함수 추가
                                  // ...
            }

        }
        
        // 어떤 콤보도 안 맞음
        Debug.Log("✗ Invalid! - Reset Combo");
        ResetCombo();
        
    }
    
    bool MatchesCombo(ComboData comboData)
    {
        if (comboData == null) return false;
        
        for (int i = 0; i < currentCombo.Count; i++)
        {
            if (i >= comboData.comboSequence.Count || 
                currentCombo[i] != comboData.comboSequence[i])
            {
                return false;
            }
        }
        return true;
    }
    
    void ExecuteFinisher(ComboData comboData)
    {
        Debug.Log($">>> Executing Finisher: {comboData.finisherSkill}");
        // 피니쉬 스킬 실행 로직
        
        Invoke(nameof(ResetCombo), 1f);  // 1초 후 리셋
    }
    
    void ResetCombo()
    {
        currentCombo.Clear();
        Debug.Log("--- Combo Reset ---");
    }

    void ExecuteAttack()
    {
        weaponHitbox.EnableHit();  // 히트박스 활성화
                                   // 애니메이션 이벤트를 통해 DisableHit()도 호출
    }
}