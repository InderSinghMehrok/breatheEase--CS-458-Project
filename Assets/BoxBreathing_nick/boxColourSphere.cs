using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boxColourSphere : MonoBehaviour
{
    private Renderer sphereRenderer; // Sets renderer of sphere

    // Sets start time
    float startTime;
    void OnEnable() {
        startTime = Time.time;

        sphereRenderer = GetComponent<Renderer>();
    }
    
    // Sets colour of sphere to shift from red to blue back to red in accordance with box breathe exercise
    void Update()
    {
        float time = (Time.time - startTime) % 16;

        if (time < 4) {
            sphereRenderer.material.color = Color.Lerp(Color.red, Color.blue, (float)(time / 4));
        } else if (time < 8) {
            sphereRenderer.material.color = Color.blue;
        } else if (time < 12) {
            sphereRenderer.material.color = Color.Lerp(Color.blue, Color.red, (float)((time - 8) / 4));
        } else {
            sphereRenderer.material.color = Color.red;
        }

    }
}