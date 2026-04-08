using UnityEngine;
using UnityEngine.SceneManagement;

public enum RoutineState
{
    ControllersDown, ReadyPrompt, Pose1, Pose2, FinishPrompt, Exit
}

[RequireComponent(typeof(AudioSource))]
public class RoutineManager : MonoBehaviour
{
    [Header("Dependencies")]
    public HeadNodDetector nodDetector;
    public PoseDetector poseDetector;
    public PassthroughController passthrough;

    [Header("Scene")]
    public GameObject backgroundSphere;

    [Header("Pose Models")]
    public GameObject[] poses;   // [0] = Cobra, [1] = Downward Dog

    [Header("Audio")]
    public AudioClip welcomeAudio;
    public AudioClip readyToBeginAudio;
    public AudioClip pose1Audio;
    public AudioClip pose2Audio;
    public AudioClip finishAudio;
    public AudioClip nodToCloseAudio;
    public AudioClip greatNextPoseAudio;  // "Great! Let's move to the next pose"

    [Header("UI")]
    public GameObject controllersDownPrompt;
    public GameObject readyPrompt;
    public GameObject finishPrompt;
    public GameObject grabControllerPrompt;
    public GameObject uiFeedback;
    public GameObject promptBackground;  // ← your single background image

    [Header("Pose Hold Settings")]
    public float poseHoldRequired = 2f;

    [Tooltip("How far upside-down the headset must be for Downward Dog (0 = any tilt, -1 = fully inverted). Default -0.5 = roughly 120° forward tilt)")]
    public float upsideDownThreshold = -0.5f;

    public RoutineState state = RoutineState.ControllersDown;
    private AudioSource audioSource;
    private bool poseHeld = false;
    private float poseHoldTime = 0f;
    private bool transitioningPose = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        SetAllPosesActive(false);
        if (promptBackground != null)
            promptBackground.SetActive(false);
        StartCoroutine(InitAfterOVRReady());
    }

    System.Collections.IEnumerator InitAfterOVRReady()
    {
        // Wait one frame so OVR/passthrough systems fully initialize
        yield return null;
        Debug.Log("[RoutineManager] OVR ready — entering ControllersDown");
        EnterState(RoutineState.ControllersDown);
    }

    void Update()
    {
        switch (state)
        {
            case RoutineState.Pose1:
                if (!transitioningPose)
                {
                    if (poseDetector.IsCobraPoseDetected())
                        HandlePoseHeld();
                    else
                        poseHeld = false;
                }
                break;

            case RoutineState.Pose2:
                if (!transitioningPose)
                {
                    if (poseDetector.IsDownwardDogDetected() && IsHeadsetUpsideDown())
                        HandlePoseHeld();
                    else
                        poseHeld = false;
                }
                break;

            case RoutineState.Exit:
                if (OVRInput.GetDown(OVRInput.Button.Any))
                    LoadMainScene();
                break;
        }
    }

    /// <summary>
    /// Returns true when the VR headset is physically inverted (de cabeça pra baixo).
    /// In Downward Dog the user's head points toward the floor, flipping the
    /// camera's world-space up vector so its Y component becomes negative.
    /// </summary>
    bool IsHeadsetUpsideDown()
    {
        if (Camera.main == null) return false;
        float headUpY = Camera.main.transform.up.y;
        Debug.Log($"[RoutineManager] Headset up.y = {headUpY:F2}  (threshold: {upsideDownThreshold})");
        return headUpY <= upsideDownThreshold;
    }

    void HandlePoseHeld()
    {
        if (!poseHeld) { poseHeld = true; poseHoldTime = 0f; }
        poseHoldTime += Time.deltaTime;

        if (poseHoldTime >= poseHoldRequired)
        {
            poseHeld = false;
            transitioningPose = true;
            uiFeedback.SetActive(true);

            RoutineState nextState = (state == RoutineState.Pose1)
                ? RoutineState.Pose2
                : RoutineState.FinishPrompt;

            StartCoroutine(PoseSuccessDelay(nextState));
        }
    }

    System.Collections.IEnumerator PoseSuccessDelay(RoutineState nextState)
    {
        // Play the "great" audio, then wait 5 seconds before moving on
        if (greatNextPoseAudio != null)
        {
            audioSource.clip = greatNextPoseAudio;
            audioSource.Play();
        }

        yield return new WaitForSeconds(5f);

        transitioningPose = false;
        EnterState(nextState);
    }

    // Shows or hides the shared background image alongside any prompt
    void ShowPromptBackground(bool show)
    {
        if (promptBackground != null)
            promptBackground.SetActive(show);
    }

    void EnterState(RoutineState newState)
    {
        state = newState;
        Debug.Log($">>> Entering state: {newState}");

        // Reset all UI
        controllersDownPrompt.SetActive(false);
        readyPrompt.SetActive(false);
        finishPrompt.SetActive(false);
        grabControllerPrompt.SetActive(false);
        uiFeedback.SetActive(false);
        ShowPromptBackground(false);

        switch (newState)
        {
            case RoutineState.ControllersDown:
                nodDetector.OnNodDetected = () => EnterState(RoutineState.ReadyPrompt);
                nodDetector.StartListening();
                passthrough.ShowRealWorld();
                backgroundSphere.SetActive(false);
                ShowPromptBackground(true);
                controllersDownPrompt.SetActive(true);
                audioSource.clip = welcomeAudio;
                audioSource.Play();
                break;

            case RoutineState.ReadyPrompt:
                nodDetector.OnNodDetected = () => {
                    poseDetector.CalibrateBaseline();
                    EnterState(RoutineState.Pose1);
                };
                nodDetector.StartListening();
                passthrough.FadeOut(1.5f);
                backgroundSphere.SetActive(true);
                ShowPromptBackground(true);
                readyPrompt.SetActive(true);
                audioSource.clip = readyToBeginAudio;
                audioSource.Play();
                break;

            case RoutineState.Pose1:
                ShowPromptBackground(false);
                ShowPose(0);
                audioSource.clip = pose1Audio;
                audioSource.Play();
                break;

            case RoutineState.Pose2:
                ShowPromptBackground(false);
                ShowPose(1);
                audioSource.clip = pose2Audio;
                audioSource.Play();
                break;

            case RoutineState.FinishPrompt:
                nodDetector.OnNodDetected = () => EnterState(RoutineState.Exit);
                nodDetector.StartListening();
                SetAllPosesActive(false);
                ShowPromptBackground(true);
                finishPrompt.SetActive(true);
                audioSource.clip = finishAudio;
                audioSource.Play();
                break;

            case RoutineState.Exit:
                passthrough.FadeIn(1.5f);
                backgroundSphere.SetActive(false);
                ShowPromptBackground(true);
                grabControllerPrompt.SetActive(true);
                audioSource.clip = nodToCloseAudio;
                audioSource.Play();
                break;
        }
    }

    void ShowPose(int index)
    {
        SetAllPosesActive(false);
        if (poses != null && index >= 0 && index < poses.Length)
            if (poses[index] != null)
                poses[index].SetActive(true);
    }

    void SetAllPosesActive(bool active)
    {
        if (poses == null) return;
        foreach (var p in poses)
            if (p != null) p.SetActive(active);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}