using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public StageData data;

    private bool selected = false;

    public Image thumbnailImage;
    public Text titleText;

    public void Init(StageData stage)
    {
        data = stage;
        thumbnailImage.sprite = stage.thumbnail;
        titleText.text = stage.stageName;
        selected = false;
    }

    public void OnClick()
    {
        if (!selected)
        {
            CardManager.Instance.SelectCard(this);
            selected = true;
        }
        else
        {
            SceneManager.LoadScene(data.sceneName);
        }
    }

    public void Deselect()
    {
        selected = false;
    }
}
