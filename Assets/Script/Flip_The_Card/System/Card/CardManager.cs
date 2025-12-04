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
    [SerializeField] private int cardCount = 4;            // 생성할 카드 개수
    [SerializeField] private float cardSpacing = 3f;       // 카드 간격
    
    [Header("Stage Data Pool")]
    [SerializeField] private List<StageData> allStageData; // 모든 스테이지 데이터
    
    private List<Card> spawnedCards = new List<Card>();    // 생성된 카드들


    void Start()
    {
        if (GameData.Instance == null)
        {
            Debug.LogError("GameData가 없습니다!");
            return;
        }
        
        // 카드 생성
        SpawnCards();
    }


    /// <summary>
    /// 카드 생성 및 StageData 할당
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

    // 시드로 랜덤 초기화
    Random.InitState(GameData.Instance.currentSeed);
    
    // 랜덤으로 StageData 선택
    List<StageData> selectedStages = GetRandomStages(cardCount);
    
    int cardsPerRow = 5;  // 한 줄에 5개
    float rowSpacing = 3f; // 줄 간격
    
    // 카드 생성
    for (int i = 0; i < selectedStages.Count; i++)
    {
        // 카드 생성
        GameObject cardObj = Instantiate(cardPrefab, cardParent);
        Card card = cardObj.GetComponent<Card>();
        
        // 현재 카드가 몇 번째 줄인지 계산
        int row = i / cardsPerRow;  // 0, 0, 0, 0, 0, 1, 1, 1...
        
        // 현재 줄에서 몇 번째인지 계산
        int col = i % cardsPerRow;  // 0, 1, 2, 3, 4, 0, 1, 2...
        
        // 위치 설정
        float x = col * cardSpacing;      // 가로 위치
        float y = row * rowSpacing;       // 세로 위치
        
        cardObj.transform.localPosition = new Vector3(x, y, 0);
        
        // StageData 할당
        card.Initialize(selectedStages[i]);
        
        // 리스트에 추가
        spawnedCards.Add(card);
    }
    
    Debug.Log($"[CardManager] 카드 {selectedStages.Count}개 생성 완료");
}


    /// <summary>
    /// 랜덤으로 StageData 선택
    /// </summary>
    List<StageData> GetRandomStages(int count)
    {
        List<StageData> result = new List<StageData>();
        List<StageData> tempList = new List<StageData>(allStageData);
        
        // 요청 개수와 실제 개수 중 작은 값
        count = Mathf.Min(count, tempList.Count);
        
        for (int i = 0; i < count; i++)
        {
            // 랜덤 선택
            int randomIndex = Random.Range(0, tempList.Count);
            result.Add(tempList[randomIndex]);
            
            // 중복 방지 (선택한 것은 제거)
            tempList.RemoveAt(randomIndex);
        }
        
        return result;
    }


    /// <summary>
    /// 카드가 선택되었을 때 호출됨 (나중에 구현)
    /// </summary>
    public void OnCardSelected(Card selectedCard)
    {
        Debug.Log($"[CardManager] 카드 선택됨");
        
        // TODO: 다른 카드들 비활성화
    }
    
}