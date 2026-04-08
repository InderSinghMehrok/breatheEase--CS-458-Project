using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gongFour : MonoBehaviour
{
    // Audio variables
    public AudioSource audioSource;
    public AudioClip gongNoise;

    // Coroutine that runs when object is enables
    void OnEnable()
    {
        StartCoroutine(gong());
    }

    // Plays gong sound in 4, 7, and 8 second intervals on a loop
    IEnumerator gong() {
        audioSource.clip = gongNoise;
        while (true) {
            audioSource.Play();
            yield return new WaitForSeconds(4f);
            audioSource.Play();
            yield return new WaitForSeconds(7f);
            audioSource.Play();
            yield return new WaitForSeconds(8f);
        }
    }
}
