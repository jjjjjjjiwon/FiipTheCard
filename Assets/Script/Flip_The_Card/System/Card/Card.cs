using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// 카드 오브젝트 (완성 버전)
/// 마우스 호버, 클릭, 뒤집기 애니메이션을 처리
/// </summary>
public class Card : MonoBehaviour
{
    [Header("Card Sides")]
    [SerializeField] private GameObject frontSide;  // 카드 앞면 (스테이지 정보 표시)
    [SerializeField] private GameObject backSide;   // 카드 뒷면 (초기 상태)
    
    [Header("Front Side UI")]
    [SerializeField] private MeshRenderer cardImageRenderer;  // 스테이지 이미지 표시용
    [SerializeField] private TextMeshPro nameText;            // 스테이지 이름
    [SerializeField] private TextMeshPro descText;            // 스테이지 설명
    
    [Header("Visual Effects")]
    [SerializeField] private Material normalMaterial;      // 기본 Material
    [SerializeField] private Material highlightMaterial;   // 호버 시 강조 Material
    
    [Header("Animation Settings")]
    [SerializeField] private float flipDuration = 0.5f;    // 뒤집기 애니메이션 시간

    private MeshRenderer cardRenderer;   // 카드 본체 Renderer (Material 변경용)
    private StageData stageData;         // 할당받은 스테이지 데이터
    private bool isSelected = false;     // 첫 번째 클릭 완료 여부
    private bool isFlipped = false;      // 카드가 뒤집혔는지 여부
    private bool canInteract = true;     // 상호작용 가능 여부 (다른 카드 선택 시 false)
    
    
    void Awake()
    {
        // 컴포넌트 캐싱
        cardRenderer = GetComponent<MeshRenderer>();
    }
    
    void Start()
    {
        // 초기 상태: 뒷면만 보임
        ShowBack();
    }
    
    
    /// <summary>
    /// 카드 초기화 - CardManager가 생성 직후 호출
    /// StageData를 받아서 UI에 세팅
    /// </summary>
    /// <param name="data">할당할 스테이지 데이터</param>
    public void Initialize(StageData data)
    {
        stageData = data;
        SetupFrontSide();
        
        Debug.Log($"[Card] 초기화: {data.stageName}");
    }
    
    /// <summary>
    /// 상호작용 비활성화
    /// 다른 카드가 선택되었을 때 CardManager가 호출
    /// </summary>
    public void DisableInteraction()
    {
        canInteract = false;
        
        // Material을 기본으로 되돌림
        if (normalMaterial != null && cardRenderer != null)
        {
            cardRenderer.material = normalMaterial;
        }
    }
    
    
    /// <summary>
    /// StageData의 정보를 앞면 UI에 세팅
    /// 아직 뒷면이라 보이지는 않음
    /// </summary>
    void SetupFrontSide()
    {
        if (stageData == null) return;
        
        // 스테이지 이미지 설정
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
    /// 카드 앞면 표시
    /// 뒤집기 애니메이션 중간에 호출
    /// </summary>
    void ShowFront()
    {
        if (frontSide != null) frontSide.SetActive(true);
        if (backSide != null) backSide.SetActive(false);
    }
    
    /// <summary>
    /// 카드 뒷면 표시
    /// 초기 상태
    /// </summary>
    void ShowBack()
    {
        if (frontSide != null) frontSide.SetActive(false);
        if (backSide != null) backSide.SetActive(true);
    }
    
    /// <summary>
    /// 첫 번째 클릭 처리
    /// 카드 선택 및 뒤집기
    /// </summary>
    void FirstClick()
    {
        isSelected = true;
        
        // 뒤집기 애니메이션 시작
        if (!isFlipped)
        {
            StartCoroutine(FlipCard());
        }
        
        // CardManager에게 선택 알림
        CardManager cardManager = FindObjectOfType<CardManager>();
        if (cardManager != null)
        {
            cardManager.OnCardSelected(this);
        }
        
        Debug.Log($"[Card] 선택됨: {stageData.stageName}");
    }

    /// <summary>
    /// 두 번째 클릭 처리
    /// 스테이지 시작
    /// </summary>
    void SecondClick()
    {
        Debug.Log($"[Card] 스테이지 시작: {stageData.stageName}");

        // 다음 층으로 이동
        GameData.Instance.NextFloor();

        // StageManager로 씬 로드
        StageManager stageManager = FindObjectOfType<StageManager>();
        if (stageManager != null)
        {
            stageManager.LoadStage(stageData);
        }
        else
        {
            Debug.LogError("[Card] StageManager를 찾을 수 없습니다!");
        }
    }


    /// <summary>
    /// 마우스가 카드 위로 올라왔을 때
    /// Unity의 OnMouseEnter 이벤트
    /// </summary>
    void OnMouseEnter()
    {
        // 상호작용 불가능하면 무시
        if (!canInteract) return;
        
        // 강조 Material 적용
        if (highlightMaterial != null && cardRenderer != null)
        {
            cardRenderer.material = highlightMaterial;
        }
    }
    
    /// <summary>
    /// 마우스가 카드에서 벗어났을 때
    /// Unity의 OnMouseExit 이벤트
    /// </summary>
    void OnMouseExit()
    {
        // 상호작용 불가능하면 무시
        if (!canInteract) return;
        
        // 선택되지 않은 카드만 기본 Material로 복구
        if (!isSelected && normalMaterial != null && cardRenderer != null)
        {
            cardRenderer.material = normalMaterial;
        }
    }
    
    /// <summary>
    /// 마우스 클릭 시
    /// Unity의 OnMouseDown 이벤트
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
    
    
    /// <summary>
    /// 카드 뒤집기 애니메이션
    /// Y축 기준 180도 회전
    /// </summary>
    IEnumerator FlipCard()
    {
        float elapsed = 0f;  // 경과 시간
        Quaternion startRotation = transform.rotation;  // 시작 회전
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 180, 0);  // 목표 회전 (180도)
        
        while (elapsed < flipDuration)
        {
            elapsed += Time.deltaTime;  // 시간 누적
            
            // 정규화된 시간 (0~1 비율)
            // 프레임마다 deltaTime이 다르므로 비율로 변환
            float t = elapsed / flipDuration;
            
            // 회전 보간 (Lerp: 선형 보간)
            // 시작 회전에서 끝 회전까지 부드럽게 변환
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            
            // 애니메이션 중간 지점(50%)에서 앞/뒷면 전환
            // 이 시점에 전환해야 자연스러움
            if (t >= 0.5f && !isFlipped)
            {
                ShowFront();
                isFlipped = true;
            }
            
            yield return null;  // 다음 프레임까지 대기
        }
        
        // 최종 회전 확정 (정확한 180도)
        transform.rotation = endRotation;
    }
    
}