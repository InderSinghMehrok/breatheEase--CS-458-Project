using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class lineScript : MonoBehaviour
{
    // This script is based on the one provided in the tutorial notes for assignment 3

    // Creates line and button
    public LineRenderer line;

    // Update is called once per frame
    void Update()
    {
        // Sets the position of the start of the line for the end to be added onto given the case
        line.SetPosition(0, transform.position);

        // Creates a ray
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.up);

        if (Physics.Raycast(ray, out hit, 100)) { // If the ray hits something...
            line.SetPosition(1, hit.point); // Sets the end of the line to what it hit
            if (hit.collider.CompareTag("button")) { // If the ray hits a button...
                Button button = hit.collider.GetComponent<Button>(); // Set variable button to the button that is hit
                button.Select();

                // Executes button event
                if(OVRInput.GetDown(OVRInput.Button.Any)) {
                    ExecuteEvents.Execute(button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
                }
            }
        } 
        
        else { // If no object was hit...
            EventSystem.current.SetSelectedGameObject(null); // Sets the hit object to null and acts as a deselect for the button
            line.SetPosition(1, transform.position + (transform.up * 20)); // Draws the line ahead 20 units.
        }

    }
}
