using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class AppStateManager : MonoBehaviour
{
    public static AppStateManager Instance;

    [Header("Panels")]
    public CanvasGroup introPanelGroup;
    public CanvasGroup exercisePanelGroup;

    [HideInInspector] public SessionData selectedSession;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        ShowIntro();
    }

    public void ShowIntro()
    {
        introPanelGroup.alpha = 1f;
        introPanelGroup.interactable = true;
        introPanelGroup.blocksRaycasts = true;

        exercisePanelGroup.alpha = 0f;
        exercisePanelGroup.interactable = false;
        exercisePanelGroup.blocksRaycasts = false;
    }

    public void ShowExercisePicker()
    {
         if (introPanelGroup == null || exercisePanelGroup == null)
    {
        Debug.LogError("Panel groups not assigned!");
        return;
    }

    introPanelGroup.alpha = 0f;
    introPanelGroup.interactable = false;
    introPanelGroup.blocksRaycasts = false;

    exercisePanelGroup.alpha = 1f;
    exercisePanelGroup.interactable = true;
    exercisePanelGroup.blocksRaycasts = true;

    Debug.Log("Showing exercise picker");
    }

    public void LoadSelectedSession()
    {
        if (selectedSession == null)
    {
        Debug.LogWarning("No session selected!");
        return;
    }
    SceneFader.Instance.FadeToScene(selectedSession.sceneToLoad);
    }

    public void OnSessionSelected(SessionData session)
    {
        selectedSession = session;
        Debug.Log("Selected: " + session.sessionName);
    }
}