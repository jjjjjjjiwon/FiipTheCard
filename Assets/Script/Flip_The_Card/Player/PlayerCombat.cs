using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<ComboData> comboDatas;
    public WeaponHitbox weaponHitbox;
    private List<KeyCode> currentCombo = new List<KeyCode>();

    // UI 표시용: 현재 피니셔 스킬
    private string currentFinisherSkill = null;

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

        ComboData matchedCombo = null;

        // 현재 입력이 어느 콤보의 '앞부분'과 일치하는지 체크
        foreach (var combo in comboDatas)
        {
            if (MatchesCombo(combo))
            {
                matchedCombo = combo;
                break;
            }
        }

        // 어떤 콤보에도 맞지 않음 → 리셋
        if (matchedCombo == null)
        {
            Debug.Log("✗ Invalid - Reset Combo");
            ResetCombo();
            return;
        }

        Debug.Log($"✓ Valid! Matching: {matchedCombo.comboName}");

        // 콤보 완성인가?
        if (currentCombo.Count == matchedCombo.comboSequence.Count)
        {
            Debug.Log($">>> COMBO COMPLETE: {matchedCombo.comboName}");
            ExecuteFinisher(matchedCombo);
            return;
        }

        // 아직 콤보 진행 중 → 공격 실행
        ExecuteAttack();
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
        currentFinisherSkill = comboData.finisherSkill; // 저장
        Debug.Log($">>> Executing Finisher: {comboData.finisherSkill}");
        // 피니쉬 스킬 실행 로직
        
        Invoke(nameof(ResetCombo), 1f);  // 1초 후 리셋
        Invoke(nameof(ClearFinisherSkill), 1f); // UI용
    }

    // UI용 필살기 null 하기
    void ClearFinisherSkill()
    {
        currentFinisherSkill = null;
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


    public string GetComboDisplay()
    {

    if(!string.IsNullOrEmpty(currentFinisherSkill))
    {
        string temp = currentFinisherSkill; // 임시 저장
        return temp;
    }

    if (currentCombo.Count == 0)
        return "aaaaa";
    
    return string.Join(" → ", currentCombo);

    }

    
}