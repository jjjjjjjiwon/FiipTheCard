using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [Header("Card Settings")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardParent;
    [SerializeField] private int cardCount = 3;
    [SerializeField] private float cardSpacing = 3f;
    
    [Header("Stage Data by Floor")]
    [SerializeField] private List<StageDataPool> stagePoolsByFloor;  // 층별 스테이지 풀
    
    private List<Card> spawnedCards = new List<Card>();

    void Start()
    {
        if (GameData.Instance == null)
        {
            Debug.LogError("GameData가 없습니다!");
            return;
        }
        
        SpawnCards();
    }

    void SpawnCards()
    {
        int currentFloor = GameData.Instance.currentFloor;
        
        // 현재 층에 맞는 스테이지 풀 가져오기
        if (currentFloor > stagePoolsByFloor.Count)
        {
            Debug.LogError($"{currentFloor}층의 스테이지 풀이 없습니다!");
            return;
        }
        
        StageDataPool pool = stagePoolsByFloor[currentFloor - 1];
        
        // 시드 설정
        Random.InitState(GameData.Instance.currentSeed + currentFloor);  // 층마다 다른 시드
        
        // 랜덤 선택
        List<StageData> selectedStages = GetRandomStages(pool.stages, cardCount);
        
        // 카드 생성
        for (int i = 0; i < selectedStages.Count; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            Card card = cardObj.GetComponent<Card>();
            
            cardObj.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);
            card.Initialize(selectedStages[i]);
            
            spawnedCards.Add(card);
        }
        
        Debug.Log($"{currentFloor}층 카드 생성 완료");
    }

    List<StageData> GetRandomStages(List<StageData> pool, int count)
    {
        List<StageData> result = new List<StageData>();
        List<StageData> tempList = new List<StageData>(pool);
        
        count = Mathf.Min(count, tempList.Count);
        
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            result.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }
        
        return result;
    }

    public void OnCardSelected(Card selectedCard)
    {
        foreach (Card card in spawnedCards)
        {
            if (card != selectedCard)
            {
                card.DisableInteraction();
            }
        }
    }
}

/// <summary>
/// 층별 스테이지 데이터 풀
/// </summary>
[System.Serializable]
public class StageDataPool
{
    public int floor;                    // 층 번호
    public List<StageData> stages;       // 이 층에서 나올 수 있는 스테이지들
}