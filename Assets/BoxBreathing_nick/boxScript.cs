using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boxScript : MonoBehaviour
{
    public int speed; // Speed variable

    public Rigidbody rb; // Rigidbody of sphere

    // Sets start time
    float startTime;
    void OnEnable() {

        startTime = Time.time;
    }

    // Sets velocity of sphere to draw a box in accordance with box breath exercise
    void Update()
    {
        int time = (int)(Time.time - startTime) % 16;

        if (time < 4) {
            rb.linearVelocity = new Vector3(0, speed, 0);
        } else if (time < 8) {
            rb.linearVelocity = new Vector3(speed, 0, 0);
        } else if (time < 12) {
            rb.linearVelocity = new Vector3(0, -speed, 0);
        } else {
            rb.linearVelocity = new Vector3(-speed, 0, 0);
        }

    }
}