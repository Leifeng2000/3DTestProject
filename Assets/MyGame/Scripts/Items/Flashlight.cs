using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashlightLight;
    private bool isOn = false;

    public bool IsOn => isOn;
    public float range => flashlightLight.range;
    public float spotAngle => flashlightLight.spotAngle;

    void Start()
    {
        flashlightLight = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    public void ToggleFlashlight()
    {
        if (flashlightLight != null)
        {
            isOn = !isOn;
            flashlightLight.enabled = isOn;

            GameObject[] lights = GameObject.FindGameObjectsWithTag("Light");
            foreach (GameObject light in lights)
            {
                Light lightComponent = light.GetComponent<Light>();
                if (lightComponent != null)
                {
                    lightComponent.enabled = isOn;
                }
            }
        }
    }
}
