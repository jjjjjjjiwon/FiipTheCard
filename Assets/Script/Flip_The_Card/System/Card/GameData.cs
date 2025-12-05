using UnityEngine;
using System.Collections.Generic;

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

    [Header("Card Management")]
    public List<StageData> selectedStages = new List<StageData>();  // 처음 선택된 카드들 (null이면 클리어됨)
    public List<StageData> clearedStages = new List<StageData>();   // 클리어한 카드들 (묘지)

    // 싱글톤 설정
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환되어도 파괴 안 됨
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
    /// ++++++++++++++++++++++++++++++++++++++++++++++++++++나중에 조절, 층이 아닌 카드가 선택되면 호출하고 있음++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// </summary>
    public void NextFloor()
    {
        currentFloor++;
        Debug.Log($"다음 층으로: {currentFloor}층");
    }

    /// <summary>
    /// 선택한 카드를 묘지로 이동
    /// Card가 두 번째 클릭될 때 호출
    /// </summary>
    /// <param name="index">선택한 카드의 인덱스 (selectedStages의 몇 번째)</param>
    public void MoveToGraveyard(int index)
    {
        // 인덱스 범위 체크
        if (index < 0 || index >= selectedStages.Count)
        {
            Debug.LogError($"[GameData] 잘못된 인덱스: {index}");
            return;
        }
        
        StageData selected = selectedStages[index];
        
        // null이 아니면 (아직 클리어 안 한 카드면)
        if (selected != null)
        {
            // 묘지에 추가
            clearedStages.Add(selected);
            
            // 원래 위치는 null로 변경 (빈 공간 표시)
            selectedStages[index] = null;
            
            Debug.Log($"[GameData] '{selected.stageName}' 묘지로 이동 (인덱스: {index})");
        }
        else
        {
            Debug.LogWarning($"[GameData] 인덱스 {index}는 이미 null입니다 (이미 클리어됨)");
        }
    }

    /// <summary>
    /// 새로운 런 시작
    /// 모든 데이터 초기화 및 시드 재생성
    /// </summary>
    public void StartNewRun()
    {
        currentFloor = 1;
        selectedStages.Clear();
        clearedStages.Clear();
        InitializeSeed();
        Debug.Log("[GameData] 새로운 런 시작!");
    }

}