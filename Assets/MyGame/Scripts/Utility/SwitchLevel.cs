using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    // Name of the scene to switch to
    public string sceneName;
    // Interaction distance
    public float interactionDistance = 3.0f;

    void Update()
    {
        // Check if the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Create a ray from the center of the screen
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            // Check if the ray hits an object within the interaction distance
            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                // Check if the hit object has the "Interactable" tag
                if (hit.collider.CompareTag("NextLevel"))
                {
                    SwitchToScene();
                }
            }
        }
    }

    public void SwitchToScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
