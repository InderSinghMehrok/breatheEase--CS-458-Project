using UnityEngine;

public enum ExerciseType { GuidedMeditation, ForestStretch }

[CreateAssetMenu(fileName = "NewSession", menuName = "Stillness/Session Data")]
public class SessionData : ScriptableObject
{
    [Header("Identity")]
    public string sessionName;
    public ExerciseType exerciseType;

    [Header("Scene")]
    public string sceneToLoad;

    [Header("Duration")]
    public int durationMinutes;

    [Header("Display")]
    [TextArea] public string description;
    public string durationLabel;
    public string difficultyLabel;
}