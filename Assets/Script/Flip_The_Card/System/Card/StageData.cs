using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("Stage Info")]
    public int stageID;
    public string stageName;
    [TextArea(3, 5)]
    public string stageDescription;
    public string iconPath;        // "StageIcons/volcano"
    public int difficulty;
    
    [Header("Scene Info")]
    public string sceneName;
    
    // 런타임에 로드될 Sprite (JSON에는 없음)
    [HideInInspector]
    public Sprite stageIcon;
}