using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tenText : MonoBehaviour
{
    public TextMeshProUGUI myText; // Creates the variable for the text

    // Sets start time
    float startTime;
    void OnEnable() {
        startTime = Time.time;
    }
    // Counts up to ten every second, resetting when ten is reached, and setting the color to osolate between red and blue (matching inhale and exhale)
    void Update()
    {
        float time = (((Time.time - startTime) / Mathf.PI) % 10) + 1;

        myText.text = ((int)(time)).ToString();

        if ((int)(time) % 2 == 1) {
            myText.faceColor = Color.Lerp(Color.red, Color.blue, (float)(time % 1));
        } else {
            myText.faceColor = Color.Lerp(Color.blue, Color.red, (float)(time % 1));
        }

    }
}
