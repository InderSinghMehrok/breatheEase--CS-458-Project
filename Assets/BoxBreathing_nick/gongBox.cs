using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gongBox : MonoBehaviour
{
    // Audio variables
    public AudioSource audioSource;
    public AudioClip gongNoise;

    // Coroutine that runs when object is enabled
    void OnEnable()
    {
        StartCoroutine(gong());
    }

    IEnumerator gong() {
        audioSource.clip = gongNoise;
        while (true) {
            audioSource.Play();
            yield return new WaitForSeconds(4f); // Runs gong sound every pi seconds
        }
    }
}