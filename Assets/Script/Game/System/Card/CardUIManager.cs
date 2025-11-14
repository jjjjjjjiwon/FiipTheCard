using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    public static CardUIManager Instance;

    public Image thumbnail;
    public Text titleText;
    public Text descText;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(StageData data)
    {
        thumbnail.sprite = data.thumbnail;
        titleText.text = data.stageName;
        descText.text = data.description;
    }
}
