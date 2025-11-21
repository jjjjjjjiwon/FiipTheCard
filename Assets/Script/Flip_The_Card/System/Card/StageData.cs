using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("Stage Info")]
    public int stageID; // 스테이지 ID
    public string stageName;    // 스테이지 이름
    [TextArea(3, 5)]
    public string stageDescription; // 스테이지 설명
    public Sprite stageIcon;    // 스테이지 이미지
    public int difficulty;  //스테이지 난이도   , 필요할까?
    
    [Header("Scene Info")]
    public string sceneName;    // 이동할 씬 이름
}