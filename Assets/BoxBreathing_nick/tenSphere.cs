using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tenSphere : MonoBehaviour
{
    // Setting variables
    float startTime, yVel;
    public int speed, amplitude;
    public Rigidbody rb;

    private Renderer sphereRenderer;

    // Sets start time and renderer of sphere
    void OnEnable()
    {
        startTime = Time.time;

        sphereRenderer = GetComponent<Renderer>();
    }

    // Logic for setting color and velocity of sphere that's height follows an osolating sine curve. 
    void Update()
    {
        float time = (Time.time - startTime);

        yVel = Mathf.Sin(time * speed) * amplitude; // Sets y velocity to height of sine manipulated by the speed and amplitude of the osolating wave
        rb.linearVelocity = new Vector3(0, yVel, 0);

        sphereRenderer.material.color = Color.Lerp(Color.blue, Color.red, (float)(0.5 * (1 + Mathf.Cos(time * speed)))); // Sets the color of the sphere such that when its at the top of the wave it is blue, and when its at the bottom of the curve it is red, with a gradient between
    }

}
