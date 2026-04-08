using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class fourseveneight : MonoBehaviour
{
    public TextMeshProUGUI myText; // Creates the variable for the text

    // Sets start time
    float startTime;
    void OnEnable() {
        startTime = Time.time;
    }

    // Sets text to count in accordance with 4-7-8 exercise
    void Update()
    {
        int time = (int)(Time.time - startTime) % 19;

        if (time < 4) {
            myText.text = (time + 1).ToString();
        } else if (time < 11) {
            myText.text = (time - 3).ToString();
        } else {
            myText.text = (time - 10).ToString();
        }

    }
}