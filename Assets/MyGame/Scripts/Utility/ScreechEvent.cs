using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreechEvent : MonoBehaviour
{
    public GameObject creature; // Reference to the original creature GameObject
    public GameObject creatureClone; // Reference to the clone creature GameObject
    public float flickerDuration = 2.0f; // Duration for the flickering effect
    public float lightsOffDuration = 5.0f; // Duration for the lights to remain off

    private List<Light> sceneLights = new List<Light>();
    private Camera playerCamera; // Reference to the player's camera

    private void Start()
    {
        // Find all lights tagged with "Light" in the scene
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject lightObject in lightObjects)
        {
            Light light = lightObject.GetComponent<Light>();
            if (light != null)
            {
                sceneLights.Add(light);
            }
        }

        // Find the player's camera
        playerCamera = Camera.main;

        // Ensure the clone creature is inactive at the start
        if (creatureClone != null)
        {
            creatureClone.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerJumpscare());
        }
    }

    private IEnumerator TriggerJumpscare()
    {
        // Start the flickering effect
        yield return StartCoroutine(FlickerLights());

        // Turn off all lights
        SetLightsActive(false);

        // Wait for the duration of lights off minus 1 second
        yield return new WaitForSeconds(lightsOffDuration - 1.0f);

        // Disable the original creature
        creature.SetActive(false);

        // Enable the clone creature
        if (creatureClone != null)
        {
            creatureClone.SetActive(true);
        }

        // Wait for the remaining 1 second
        yield return new WaitForSeconds(1.0f);

        // Turn the lights back on
        SetLightsActive(true);
    }

    private void Update()
    {
        // Check if the player's raycast hits the clone creature's collider
        if (creatureClone != null && creatureClone.activeSelf)
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == creatureClone)
                {
                    // Trigger the rush animation
                    Animator animator = creatureClone.GetComponent<Animator>();
                    if (animator != null)
                    {
                        animator.SetTrigger("isRush");
                    }
                }
            }
        }
    }

    private IEnumerator FlickerLights()
    {
        float endTime = Time.time + flickerDuration;
        while (Time.time < endTime)
        {
            foreach (Light light in sceneLights)
            {
                light.enabled = !light.enabled;
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }

    private void SetLightsActive(bool isActive)
    {
        foreach (Light light in sceneLights)
        {
            light.enabled = isActive;
        }
    }
}
