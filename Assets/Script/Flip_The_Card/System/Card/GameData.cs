using UnityEngine;

/// <summary>
/// 게임 전역 데이터 관리 싱글톤
/// 씬 전환되어도 유지됨 (DontDestroyOnLoad)
/// </summary>
public class GameData : MonoBehaviour
{
    /// 싱글톤 인스턴스
    public static GameData Instance { get; private set; }

    [Header("Current Session")]
    public int currentSeed;           // 현재 세션의 랜덤 시드
    public int currentFloor = 1;      // 현재 층
    public int lastSelectedStageID = -1;  // 마지막 선택한 스테이지 ID (초기값 -1)

    // 싱글톤 설정
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSeed();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// 시드 초기화 - 랜덤 시드 생성 (로그라이크 방식)
    /// </summary>
    void InitializeSeed()
    {
        currentSeed = Random.Range(int.MinValue, int.MaxValue);
        Debug.Log($"[GameData] 생성된 시드: {currentSeed}");
    }
    
    /// <summary>
    /// 다음 층으로 이동
    /// Card가 두 번째 클릭될 때 호출됨
    /// </summary>
    public void NextFloor()
    {
        currentFloor++;
        Debug.Log($"다음 층으로: {currentFloor}층");
    }

    /// <summary>
    /// 선택한 스테이지 기록
    /// Card가 선택될 때 호출
    /// </summary>
    /// <param name="stageID">선택한 스테이지 ID</param>
    public void SetLastSelectedStage(int stageID)
    {
        lastSelectedStageID = stageID;
        Debug.Log($"[GameData] 선택한 스테이지 기록: ID {stageID}");
    }
    
    /// <summary>
    /// 새로운 런 시작 - 시드 재생성 및 층 초기화
    /// </summary>
    public void StartNewRun()
    {
        currentFloor = 1;
        InitializeSeed();
        Debug.Log("새로운 런 시작!");
    }

}