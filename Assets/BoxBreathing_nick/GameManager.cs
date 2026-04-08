using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This worked when I attached it to the main camera rig
    
    private static GameManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //print("from level 2");
    }
}
