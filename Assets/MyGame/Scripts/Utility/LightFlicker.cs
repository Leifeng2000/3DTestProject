using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float flickerDurationMin = 0.05f; // Minimum duration of each flicker
    public float flickerDurationMax = 0.2f;  // Maximum duration of each flicker
    public float flickerIntervalMin = 0.3f;  // Minimum interval between flickers
    public float flickerIntervalMax = 0.7f;  // Maximum interval between flickers

    private List<Light> lights1 = new List<Light>();
    private List<Light> lights2 = new List<Light>();
    private List<Light> lights3 = new List<Light>();

    private bool isFlickering = false;

    void Start()
    {
        // Find all lights with the specified tags
        GameObject[] lightsWithTag1 = GameObject.FindGameObjectsWithTag("Light1");
        GameObject[] lightsWithTag2 = GameObject.FindGameObjectsWithTag("Light2");
        GameObject[] lightsWithTag3 = GameObject.FindGameObjectsWithTag("Light3");

        // Add Light components to respective lists
        foreach (GameObject lightObject in lightsWithTag1)
        {
            Light light = lightObject.GetComponent<Light>();
            if (light != null)
            {
                lights1.Add(light);
            }
        }

        foreach (GameObject lightObject in lightsWithTag2)
        {
            Light light = lightObject.GetComponent<Light>();
            if (light != null)
            {
                lights2.Add(light);
            }
        }

        foreach (GameObject lightObject in lightsWithTag3)
        {
            Light light = lightObject.GetComponent<Light>();
            if (light != null)
            {
                lights3.Add(light);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFlickering)
        {
            isFlickering = true;
            StartCoroutine(FlickerLights(lights1));
            StartCoroutine(FlickerLights(lights2));
            StartCoroutine(FlickerLights(lights3));
        }
    }

    IEnumerator FlickerLights(List<Light> lights)
    {
        // Initial random delay to desynchronize the start
        yield return new WaitForSeconds(Random.Range(0, flickerIntervalMax));

        while (isFlickering)
        {
            foreach (Light light in lights)
            {
                light.enabled = !light.enabled; // Toggle light
            }
            yield return new WaitForSeconds(Random.Range(flickerDurationMin, flickerDurationMax));
            foreach (Light light in lights)
            {
                light.enabled = !light.enabled; // Toggle light back
            }
            yield return new WaitForSeconds(Random.Range(flickerIntervalMin, flickerIntervalMax));
        }
    }
}
