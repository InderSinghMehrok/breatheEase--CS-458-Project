using UnityEngine;
using UnityEngine.InputSystem;

public class PoseDetector : MonoBehaviour
{
    private Transform head;
    private float baselineHeadHeight;

    void Start()
    {
        head = GameObject.Find("CenterEyeAnchor").transform;
    }

    // Call this before the routine starts to record the user's standing head height
    public void CalibrateBaseline()
    {
        baselineHeadHeight = head.position.y;
    }
// Pose 1 — Cobra
// User is lying down — head drops significantly below standing baseline
public bool IsCobraPoseDetected()
{
    if (Keyboard.current != null && Keyboard.current.digit1Key.isPressed)
    {
        Debug.Log("Cobra pose — keyboard override active");
        return true;
    }

    float heightDiff = head.position.y - baselineHeadHeight;
    float pitch = NormalizedPitch();

    Debug.Log($"Cobra pose — pitch: {pitch:F2}, heightDiff: {heightDiff:F2}");

    // Head is significantly lower than standing baseline (lying on floor)
    return heightDiff < -0.8f && pitch < 20f;
}

    // Pose 2 — Downward Dog
    // Detected when the head drops well below the baseline (user bends forward)
    public bool IsDownwardDogDetected()
    {
        // Debug override: hold '2' to simulate detection
        if (Keyboard.current != null && Keyboard.current.digit2Key.isPressed) return true;

        float heightDiff = head.position.y - baselineHeadHeight;
        // Head dropped ~30cm or more below standing position
        return heightDiff < -0.3f;
    }

    // Converts Unity's 0–360 euler pitch to a -180/+180 range
    float NormalizedPitch()
    {
        float x = head.localEulerAngles.x;
        return x > 180 ? x - 360 : x;
    }
}