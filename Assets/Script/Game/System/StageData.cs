using UnityEngine;

[CreateAssetMenu(menuName="Card/StageData")]
public class StageData : ScriptableObject
{
    public string stageName;
    public string sceneName;
    public Sprite thumbnail;
    public string description;
}