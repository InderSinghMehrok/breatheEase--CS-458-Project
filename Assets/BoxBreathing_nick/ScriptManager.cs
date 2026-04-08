using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptManager : MonoBehaviour
{
    // Loads a scene and sets that scene under sceneName
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    
}
