using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTrigger : MonoBehaviour
{
    public GameObject flashlight;
    public float flashlightOffDuration = 2.0f;
    public float bodyEnableDelay = 0f;

    private List<GameObject> bodies = new List<GameObject>();

    void Start()
    {
        // Find all objects with the "Body" tag and disable them
        GameObject[] bodyObjects = GameObject.FindGameObjectsWithTag("Body");
        foreach (GameObject body in bodyObjects)
        {
            body.SetActive(false);
            bodies.Add(body);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(HandleFlashlightAndBodies());
        }
    }

    IEnumerator HandleFlashlightAndBodies()
    {
        // Disable the flashlight
        if (flashlight != null)
        {
            flashlight.SetActive(false);
        }

        // Wait for the flashlight off duration
        yield return new WaitForSeconds(flashlightOffDuration);

        // Enable the flashlight
        if (flashlight != null)
        {
            flashlight.SetActive(true);
        }

        // Wait for the body enable delay
        yield return new WaitForSeconds(bodyEnableDelay);

        // Enable all "Body" objects
        foreach (GameObject body in bodies)
        {
            body.SetActive(true);
        }
    }
}
