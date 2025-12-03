using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// 카드 오브젝트 (간단 버전 - 세팅만)
/// 나중에 호버, 클릭 기능 추가 예정
/// </summary>
public class Card : MonoBehaviour
{   

    [Header("Card Sides")]
    [SerializeField] private GameObject frontSide;              // 카드 앞면 (스테이지 정보 표시)
    [SerializeField] private GameObject backSide;               // 카드 뒷면 (초기 상태)
    
    [Header("Front Side UI")]
    [SerializeField] private MeshRenderer cardImageRenderer;    // 스테이지 이미지를 표시할 Renderer
    [SerializeField] private TextMeshPro nameText;              // 스테이지 이름 텍스트
    [SerializeField] private TextMeshPro descText;              // 스테이지 설명 텍스트

    private StageData stageData;         // 이 카드에 할당된 스테이지 데이터

    void Start()
    {
        // 초기 상태: 뒷면만 보이게
        ShowBack();
    }
    
    /// <summary>
    /// CardManager에서 호출 - 카드에 스테이지 데이터 할당 및 UI 세팅
    /// </summary>
    public void Initialize(StageData data)
    {
        stageData = data;
        SetupFrontSide();

        Debug.Log($"[Card] 초기화: {data.stageName}");
    }
    
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
}