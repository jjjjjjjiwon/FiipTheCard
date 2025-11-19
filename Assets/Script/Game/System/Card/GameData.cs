using UnityEngine;

/// <summary>
/// 게임 전역 데이터 관리 싱글톤
/// 씬 전환되어도 유지되며, 현재 층 정보 관리
/// </summary>
public class GameData : MonoBehaviour
{
    #region Singleton
    
    public static GameData Instance { get; private set; }
    
    #endregion
    
    #region Public Fields
    
    [Header("Current Session")]
    public int currentSeed;           // 현재 세션의 시드 값
    public int currentFloor = 1;      // 현재 층 (1층부터 시작)
    
    #endregion
    
    #region Unity Lifecycle
    
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
    
    #endregion
    
    #region Private Methods
    
    /// <summary>
    /// 시드 초기화 - 랜덤 시드 생성 (로그라이크 방식)
    /// </summary>
    void InitializeSeed()
    {
        currentSeed = Random.Range(int.MinValue, int.MaxValue);
        Debug.Log($"생성된 시드: {currentSeed}");
    }
    
    #endregion
    
    #region Public Methods
    
    /// <summary>
    /// 다음 층으로 이동
    /// </summary>
    public void NextFloor()
    {
        currentFloor++;
        Debug.Log($"다음 층으로: {currentFloor}층");
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
    
    #endregion
}