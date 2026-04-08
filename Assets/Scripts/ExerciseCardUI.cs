using UnityEngine;
using UnityEngine.UI;

public class ExerciseCardUI : MonoBehaviour
{
    [Header("Session Data Assets")]
    public SessionData[] sessions;

    [Header("Card Buttons — same order as sessions")]
    public Button[] cardButtons;

    [Header("Card Backgrounds — same order as sessions")]
    public Image[] cardBackgrounds;

    public Color selectedColor = new Color(0.18f, 0.38f, 0.28f, 0.9f);
    public Color normalColor = new Color(0.08f, 0.16f, 0.12f, 0.85f);

    int _selectedIndex = -1;

    void Start()
    {
        for (int i = 0; i < sessions.Length; i++)
        {
            if (sessions[i] == null) continue;
            if (cardButtons[i] == null) continue;

            int captured = i;
            cardButtons[i].onClick.RemoveAllListeners();
            cardButtons[i].onClick.AddListener(() => SelectCard(captured));
        }
    }

    void SelectCard(int index)
    {
        _selectedIndex = index;
        AppStateManager.Instance.OnSessionSelected(sessions[index]);

        for (int i = 0; i < cardBackgrounds.Length; i++)
        {
            if (cardBackgrounds[i] == null) continue;
            cardBackgrounds[i].color = (i == index) ? selectedColor : normalColor;
        }
    }
}