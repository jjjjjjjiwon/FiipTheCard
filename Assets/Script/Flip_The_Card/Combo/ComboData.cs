using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combo", menuName = "Combat/Combo Data")]
public class ComboData : ScriptableObject
{
    public string comboName;  // 콤보 이름
    public List<KeyCode> comboSequence;  // 콤보 시퀀스
    public string finisherSkill;  // 피니쉬 스킬 이름 추가
}
