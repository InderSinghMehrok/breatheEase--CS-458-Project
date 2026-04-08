using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class collisionScript : MonoBehaviour
{
    public AudioClip sound; // Used for success sound
    private AudioSource audioSource; // Used to set audio source

    // Function used for verifying if a given game object has a given material name attached to it
    public bool materialName(GameObject obj, string materialName) {
        // Detects if the object has a renderer and therefore can contain materials. If it has a renderer, that is associated with a variable "renderer"
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer ==  null) {
            return false;
        }

        // Checks if each material in the game object renderer has the material name
        foreach (Material mat in renderer.sharedMaterials) {
            if (mat != null && mat.name.StartsWith(materialName)) {
                return true;
            }
        }

        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Finds audioSource
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function for if a collision occurs
    void OnCollisionEnter(Collision collision) {
        
        Debug.Log("Hit: " + collision.gameObject.name); // Used for debugging

        if (collision.gameObject.CompareTag("block")) { // If a block is hit...

            // If else if statement for guarenteeing that a cylinder can only destroy a block that is the same colour as it
            if (materialName(this.gameObject, "Red") && materialName(collision.gameObject, "Red")) {
                Destroy (collision.gameObject);
                audioSource.PlayOneShot(sound); // Plays sound when block is destroyed
            } else if (materialName(this.gameObject, "Blue") && materialName(collision.gameObject, "Blue")) {
                Destroy (collision.gameObject);
                audioSource.PlayOneShot(sound);
            }
        }
    }
}
