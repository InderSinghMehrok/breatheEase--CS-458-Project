using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class colourChange478 : MonoBehaviour
{
    public TextMeshProUGUI myText; // Creates the variable for the text

    // Sets start time
    float startTime;
    void OnEnable() {
        startTime = Time.time;
    }
   
   // Sets the color of the text to shift from red to blue back to red in accordance with 4-7-8 exercise
    void Update()
    {
        float time = (Time.time - startTime) % 19;

        if (time < 4.0) {
            myText.faceColor = Color.Lerp(Color.red, Color.blue, (float)(time / 4));
        } else if (time < 11.0) {
            myText.faceColor = Color.blue;
        } else {
            myText.faceColor = Color.Lerp(Color.blue, Color.red, (float)((time - 11) / 8));
        }

    }
}