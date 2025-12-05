using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카드 생성 및 관리
/// </summary>
public class CardManager : MonoBehaviour
{
    [Header("Card Settings")]
    [SerializeField] private GameObject cardPrefab;        // Card 프리팹
    [SerializeField] private Transform cardParent;         // 카드 생성될 부모 Transform
    [SerializeField] private int cardCount = 3;            // 처음 생성할 카드 개수
    [SerializeField] private float cardSpacing = 3f;       // 카드 간격
    
    [Header("Stage Data Pool")]
    [SerializeField] private List<StageData> allStageData; // 모든 스테이지 데이터
    
    private List<Card> spawnedCards = new List<Card>();    // 생성된 카드들
    
    void Start()
    {
        if (GameData.Instance == null)
        {
            Debug.LogError("[CardManager] GameData가 없습니다!");
            return;
        }
        
        SpawnCards();
    }
    
    /// <summary>
    /// 카드 생성
    /// 처음이면 랜덤 선택 후 저장, 재진입이면 저장된 것 사용
    /// </summary>
    void SpawnCards()
    {
        if (cardPrefab == null)
        {
            Debug.LogError("[CardManager] Card 프리팹이 없습니다!");
            return;
        }

        if (allStageData.Count == 0)
        {
            Debug.LogError("[CardManager] StageData가 없습니다!");
            return;
        }

        // 처음 진입 (selectedStages가 비어있음)
        if (GameData.Instance.selectedStages.Count == 0)
        {
            InitializeFirstTime();
        }
        
        // 카드 생성 (null 자리는 빈 공간)
        CreateCards();
    }
    
    /// <summary>
    /// 처음 진입 시 랜덤으로 StageData 선택 및 저장
    /// </summary>
    void InitializeFirstTime()
    {
        // 시드로 랜덤 초기화
        Random.InitState(GameData.Instance.currentSeed);
        
        // 랜덤 선택
        List<StageData> selected = GetRandomStages(cardCount);
        
        // GameData에 저장
        GameData.Instance.selectedStages = selected;
        
        Debug.Log($"[CardManager] 처음 진입 - 카드 {selected.Count}개 선택 완료");
    }
    
    /// <summary>
    /// GameData.selectedStages 기반으로 카드 생성
    /// null이면 빈 공간
    /// </summary>
    void CreateCards()
    {
        List<StageData> stages = GameData.Instance.selectedStages;
        
        int cardsPerRow = 5;
        float rowSpacing = 5f;
        
        for (int i = 0; i < stages.Count; i++)
        {
            StageData stageData = stages[i];
            
            // null이면 빈 공간 (카드 생성 안 함)
            if (stageData == null)
            {
                Debug.Log($"[CardManager] 인덱스 {i}는 null - 빈 공간");
                continue;
            }
            
            // 카드 생성
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            Card card = cardObj.GetComponent<Card>();
            
            // 위치 계산
            int row = i / cardsPerRow;
            int col = i % cardsPerRow;
            float x = col * cardSpacing;
            float y = row * rowSpacing;
            
            cardObj.transform.localPosition = new Vector3(x, y, 0);
            
            // 초기화 (인덱스도 전달)
            card.Initialize(stageData, i);
            
            spawnedCards.Add(card);
        }
        
        Debug.Log($"[CardManager] 카드 {spawnedCards.Count}개 생성 완료");
    }
    
    /// <summary>
    /// 랜덤으로 StageData 선택
    /// </summary>
    List<StageData> GetRandomStages(int count)
    {
        List<StageData> result = new List<StageData>();
        List<StageData> tempList = new List<StageData>(allStageData);
        
        count = Mathf.Min(count, tempList.Count);
        
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            result.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }
        
        return result;
    }
    
    /// <summary>
    /// 카드가 선택되었을 때 호출
    /// 다른 카드들 비활성화
    /// </summary>
    public void OnCardSelected(Card selectedCard)
    {
        Debug.Log($"[CardManager] 카드 선택됨");
        
        foreach (Card card in spawnedCards)
        {
            if (card != selectedCard)
            {
                card.DisableInteraction();
            }
        }
    }
}