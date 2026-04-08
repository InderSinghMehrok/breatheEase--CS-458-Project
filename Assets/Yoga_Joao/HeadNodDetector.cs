using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadNodDetector : MonoBehaviour
{
    [Header("Nod Settings")]
    public float nodThreshold = 15f;     // degrees of downward movement to count as a nod
    public float nodWindow = 0.8f;       // max time window for a complete nod (seconds)
    public int nodsRequired = 2;         // how many nods to confirm intent

    private Transform headTransform;
    private float baselineY;
    private int nodCount = 0;
    private bool movingDown = false;
    private float nodTimer = 0f;
 
    public Action OnNodDetected;

   void Start()
{
    headTransform = GameObject.Find("CenterEyeAnchor").transform;
    Debug.Log("HeadNodDetector Start — headTransform found: " + (headTransform != null));
}

public void StartListening()
{
    baselineY = headTransform.localEulerAngles.x;
    nodCount = 0;
    enabled = true;
    Debug.Log("HeadNodDetector — StartListening called");
}

void Update()
{
    if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
    {
        Debug.Log("HeadNodDetector — Space pressed, OnNodDetected is null: " + (OnNodDetected == null));
        OnNodDetected?.Invoke();
        return;
    }

    float currentX = headTransform.localEulerAngles.x;
    float normalizedX = currentX > 180 ? currentX - 360 : currentX;

    nodTimer += Time.deltaTime;
    if (nodTimer > nodWindow) { nodCount = 0; nodTimer = 0; }

    if (!movingDown && normalizedX > nodThreshold)
    {
        movingDown = true;
    }
    else if (movingDown && normalizedX < nodThreshold * 0.3f)
    {
        movingDown = false;
        nodCount++;
        nodTimer = 0;

        if (nodCount >= nodsRequired)
        {
            nodCount = 0;
            OnNodDetected?.Invoke();
        }
    }
}
}