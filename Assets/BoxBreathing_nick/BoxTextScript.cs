using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxTextScript : MonoBehaviour
{
    public TextMeshProUGUI myText; // Creates the variable for the text

    // Sets start time
    float startTime;
    void OnEnable() {
        startTime = Time.time;
    }

    // Sets text to say the breathing commands in accordance with box breath exercise
    void Update()
    {
        int time = (int)(Time.time - startTime) % 16;

        if (time < 4) {
            myText.text = "Breathe In";
        } else if (time < 8) {
            myText.text = "Hold";
        } else if (time < 12) {
            myText.text = "Breathe Out";
        } else {
            myText.text = "Hold";
        }

    }
}