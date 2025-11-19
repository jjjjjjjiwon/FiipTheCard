using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// 3D 카드 오브젝트 - 스테이지 선택을 위한 카드
/// 마우스 호버/클릭 처리 및 카드 뒤집기 애니메이션 담당
/// </summary>
public class Card : MonoBehaviour
{   
    #region Serialized Fields
    
    [Header("Card Sides")]
    [SerializeField] private GameObject frontSide;  // 카드 앞면 (스테이지 정보 표시)
    [SerializeField] private GameObject backSide;   // 카드 뒷면 (초기 상태)
    
    [Header("Front Side UI")]
    [SerializeField] private MeshRenderer cardImageRenderer;  // 스테이지 이미지를 표시할 Renderer
    [SerializeField] private TextMeshPro nameText;            // 스테이지 이름 텍스트
    [SerializeField] private TextMeshPro descText;            // 스테이지 설명 텍스트
    
    [Header("Visual Effects")]
    [SerializeField] private Material normalMaterial;      // 기본 상태 Material
    [SerializeField] private Material highlightMaterial;   // 마우스 호버 시 강조 Material
    
    [Header("Animation Settings")]
    [SerializeField] private float flipDuration = 0.5f;    // 카드 뒤집기 애니메이션 시간
    
    #endregion
    
    #region Private Fields
    
    private MeshRenderer cardRenderer;   // 카드 본체의 Renderer (Material 변경용)
    private Collider cardCollider;       // 마우스 클릭 감지용 Collider
    private bool isSelected = false;     // 첫 번째 클릭 완료 여부
    private bool isFlipped = false;      // 카드가 뒤집혔는지 여부
    private bool canInteract = true;     // 상호작용 가능 여부 (다른 카드 선택 시 false)
    private StageData stageData;         // 이 카드에 할당된 스테이지 데이터
    
    #endregion
    
    #region Unity Lifecycle
    
    void Start()
    {
        // 컴포넌트 참조 캐싱
        cardRenderer = GetComponent<MeshRenderer>();
        cardCollider = GetComponent<Collider>();
        
        // 초기 상태: 뒷면만 보이게
        ShowBack();
    }
    
    #endregion
    
    #region Public Methods
    
    /// <summary>
    /// CardManager에서 호출 - 카드에 스테이지 데이터 할당 및 UI 세팅
    /// </summary>
    /// <param name="data">표시할 스테이지 데이터</param>
    public void Initialize(StageData data)
    {
        stageData = data;
        SetupFrontSide();
    }
    
    /// <summary>
    /// 다른 카드가 선택되었을 때 호출 - 이 카드의 상호작용 비활성화
    /// </summary>
    public void DisableInteraction()
    {
        canInteract = false;
        
        // Material을 원래대로 되돌림
        if (normalMaterial != null && cardRenderer != null)
        {
            cardRenderer.material = normalMaterial;
        }
    }
    
    #endregion
    
    #region Private Methods
    
    /// <summary>
    /// StageData의 정보를 앞면 UI에 세팅 (아직 보이지는 않음)
    /// </summary>
    void SetupFrontSide()
    {
        if (stageData == null) return;
        
        // 카드 이미지 설정
        if (cardImageRenderer != null && stageData.stageIcon != null)
        {
            cardImageRenderer.material.mainTexture = stageData.stageIcon.texture;
        }
        
        // 스테이지 이름 설정
        if (nameText != null)
        {
            nameText.text = stageData.stageName;
        }
        
        // 스테이지 설명 설정
        if (descText != null)
        {
            descText.text = stageData.stageDescription;
        }
    }
    
    /// <summary>
    /// 첫 번째 클릭 처리 - 카드 뒤집기 및 CardManager에 알림
    /// </summary>
    void FirstClick()
    {
        isSelected = true;
        
        // 카드 뒤집기 애니메이션 시작
        if (!isFlipped)
        {
            StartCoroutine(FlipCard());
        }
        
        // CardManager에게 이 카드가 선택되었음을 알림
        CardManager cardManager = FindObjectOfType<CardManager>();      // 나중에 보고 Awake에서 초기화로 변경하기
        if (cardManager != null)
        {
            cardManager.OnCardSelected(this);
        }
        
        Debug.Log($"카드 선택됨: {stageData.stageName}");
    }

    /// <summary>
    /// 두 번째 클릭 처리 - 스테이지 시작
    /// </summary>
    void SecondClick()
    {
        Debug.Log($"스테이지 시작: {stageData.stageName}");

        // 다음 층으로 이동
        GameData.Instance.NextFloor();

        // StageManager에게 스테이지 로드 요청
        StageManager stageManager = FindObjectOfType<StageManager>();
        if (stageManager != null)
        {
            stageManager.LoadStage(stageData);
        }
    }

    /// <summary>
    /// 카드 앞면 표시
    /// </summary>
    void ShowFront()
    {
        if (frontSide != null) frontSide.SetActive(true);
        if (backSide != null) backSide.SetActive(false);
    }
    
    /// <summary>
    /// 카드 뒷면 표시
    /// </summary>
    void ShowBack()
    {
        if (frontSide != null) frontSide.SetActive(false);
        if (backSide != null) backSide.SetActive(true);
    }
    
    #endregion
    
    #region Mouse Events
    
    /// <summary>
    /// 마우스가 카드 위로 올라왔을 때 - 강조 효과
    /// </summary>
    void OnMouseEnter()
    {
        // 상호작용 불가능한 상태면 무시
        if (!canInteract) return;
        
        // 강조 Material로 변경
        if (highlightMaterial != null && cardRenderer != null)
        {
            cardRenderer.material = highlightMaterial;
        }
    }
    
    /// <summary>
    /// 마우스가 카드에서 벗어났을 때 - 강조 해제
    /// </summary>
    void OnMouseExit()
    {
        // 상호작용 불가능한 상태면 무시
        if (!canInteract) return;
        
        // 선택되지 않은 카드만 원래 Material로 복구
        if (!isSelected && normalMaterial != null && cardRenderer != null)
        {
            cardRenderer.material = normalMaterial;
        }
    }
    
    /// <summary>
    /// 마우스 클릭 처리
    /// </summary>
    void OnMouseDown()
    {
        // 상호작용 불가능하거나 데이터가 없으면 무시
        if (!canInteract || stageData == null) return;
        
        if (!isSelected)
        {
            // 첫 번째 클릭: 카드 선택
            FirstClick();
        }
        else
        {
            // 두 번째 클릭: 스테이지 시작
            SecondClick();
        }
    }
    
    #endregion
    
    #region Coroutines
    
    /// <summary>
    /// 카드 뒤집기 애니메이션 (Y축 180도 회전)
    /// </summary>
    IEnumerator FlipCard()
    {
        float elapsed = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 180, 0);
        
        while (elapsed < flipDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / flipDuration;  // 프레임마다 Time.deltaTime이 달라지기 때문에, “정규화(0~1 비율)”로 바꾼 것
            
            // 회전 보간
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            
            // 애니메이션 중간 지점(50%)에서 앞/뒷면 전환
            if (t >= 0.5f && !isFlipped)
            {
                ShowFront();
                isFlipped = true;
            }
            
            yield return null;
        }
        
        // 최종 회전 확정
        transform.rotation = endRotation;
    }
    
    #endregion
}