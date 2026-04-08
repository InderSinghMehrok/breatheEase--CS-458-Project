using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerScript : MonoBehaviour
{
    // Sets all audio and game object variables
    public AudioSource audioSource;
    public AudioClip breathe1;
    public AudioClip breathe2;
    public AudioClip breathe3;
    public AudioClip breathe4;
    public AudioClip breathe5;

    public GameObject test;
    public GameObject fourSevenEight;
    public GameObject boxBreath;
    public GameObject tenBreath;

    // Start is called before the first frame update
    void Start()
    {
        // Sets all relevent objects to unactive
        test.SetActive(false);
        fourSevenEight.SetActive(false);
        boxBreath.SetActive(false);
        tenBreath.SetActive(false);
        
        StartCoroutine(Sequence());
    }

    // Main coroutine for the experience
    IEnumerator Sequence() {

        // First section of experience
        audioSource.clip = breathe1; 
        audioSource.Play(); // Plays first voiceover clip

        yield return new WaitUntil(() => !audioSource.isPlaying); // Waits until voiceover is done 

        test.SetActive(true); // Sets test object to active

        yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.Button.Any)); // Waits until user presses any button

        test.SetActive(false); // Sets test object to unactive


        // Second section of experience (very similar to first section)
        audioSource.clip = breathe2;
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);

        fourSevenEight.SetActive(true);

        yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.Button.Any));

        fourSevenEight.SetActive(false);


        // Third section of experience
        audioSource.clip = breathe3;
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);

        boxBreath.SetActive(true);

        yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.Button.Any));

        boxBreath.SetActive(false);


        // Fource section of experience
        audioSource.clip = breathe4;
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);

        tenBreath.SetActive(true);

        yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.Button.Any));

        tenBreath.SetActive(false);

        // Final voiceover
        audioSource.clip = breathe5;
        audioSource.Play();
        yield return new WaitUntil(() => !audioSource.isPlaying);
        
        // Return to main menu
        SceneFader.Instance.FadeToScene("MainMenuScene");
    }
}
