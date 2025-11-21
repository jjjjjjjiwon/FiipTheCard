using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public void LoadStage(StageData stageData)
    {
        if (stageData == null)
        {
            Debug.LogError("StageData가 없습니다!");
            return;
        }
        
        Debug.Log($"스테이지 로드: {stageData.stageName}");
        SceneManager.LoadScene(stageData.sceneName);
    }
}