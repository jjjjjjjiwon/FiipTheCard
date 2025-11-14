using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    private Card current;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectCard(Card card)
    {
        if (current != null)
            current.Deselect();

        current = card;
        CardUIManager.Instance.Show(card.data);
    }
}
