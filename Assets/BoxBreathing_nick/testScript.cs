using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that plays a gone noise once when ran
public class testScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gongNoise;

    // Start is called before the first frame update
    void OnEnable()
    {
        audioSource.clip = gongNoise;
        audioSource.Play();
    }

}