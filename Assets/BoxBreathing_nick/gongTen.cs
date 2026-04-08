using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gongTen : MonoBehaviour
{
    // Audio variables
    public AudioSource audioSource;
    public AudioClip gongNoise;

    // Coroutine that runs when object is enables
    void OnEnable()
    {
        StartCoroutine(gong());
    }

    IEnumerator gong() {
        audioSource.clip = gongNoise;
        while (true) {
            audioSource.Play();
            yield return new WaitForSeconds(Mathf.PI); // Plays gong sound every four seconds
        }
    }
}
