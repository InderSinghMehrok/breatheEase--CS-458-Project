using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class clickScript : MonoBehaviour
{
    public string sceneName; // Variable for the name of the scene

    public void changeScene() {
        SceneManager.LoadScene(sceneName);
    }

    void Update() {
        if (OVRInput.GetDown(OVRInput.Button.One)) 
        { 
            changeScene();
        } 
    }
}
