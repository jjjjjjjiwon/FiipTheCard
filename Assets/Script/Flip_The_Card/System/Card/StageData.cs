using UnityEngine;


/// <summary>
/// 스테이지 정보를 담는 ScriptableObject
/// Project 창에서 Create → Game → Stage Data로 생성 가능
/// </summary>
[CreateAssetMenu(fileName = "New Stage", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("Stage Info")]
    public int stageID;             // 스테이지 ID
    public string stageName;        // 스테이지 이름, (카드에 표시)
    
    [TextArea(3, 5)]
    public string stageDescription; // 스테이지 설명, (카드에 표시)
    public Sprite stageIcon;        // 스테이지 이미지, (카드에 표시)
    public int difficulty;          //스테이지 난이도   , 필요할까?
    
    [Header("Scene Info")]
    public string sceneName;        // 로드할 씬 이름
}