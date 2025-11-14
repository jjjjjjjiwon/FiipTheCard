using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public StageData[] stageList;   // 여기에 StageData만 추가하면 됨
    public GameObject cardPrefab;   // 카드 UI 프리팹
    public Transform grid;        // 생성될 부모(그리드/스크롤뷰 Content)

    void Start()
    {
        foreach (var stage in stageList)
        {
            GameObject obj = Instantiate(cardPrefab, grid);
            Card card = obj.GetComponent<Card>();
            card.Init(stage);
        }
    }
}
