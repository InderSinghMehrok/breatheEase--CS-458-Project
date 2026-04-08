using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class text478 : MonoBehaviour
{
    public TextMeshProUGUI myText; // Creates the variable for the text

    // Sets start time
    float startTime;
    void OnEnable() {
        startTime = Time.time;
    }
    
    // Sets text to display breathe in hold breathe out in accordance with 4-7-8 exercise
    void Update()
    {
        int time = (int)(Time.time - startTime) % 19;

        if (time < 4) {
            myText.text = "Breathe In";
        } else if (time < 11) {
            myText.text = "Hold";
        } else {
            myText.text = "Breathe Out";
        }

    }
}
