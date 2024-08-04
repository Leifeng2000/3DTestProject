using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    public float bobSpeed = 0.05f; // Speed of the bobbing motion
    public float bobAmount = 0.05f; // Amount of bobbing motion
    public float midpoint = 0.7f; // Default camera height

    private float timer = 0.0f;
    private float waveslice;
    private float horizontal;
    private float vertical;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        Vector3 newPosition = transform.localPosition;

        // Get player input (assuming a CharacterController or similar for movement)
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Only bob the head when the player is moving
        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            // Use Time.deltaTime to ensure consistent bobbing speed
            waveslice = Mathf.Sin(timer);
            timer += bobSpeed * Time.deltaTime; // Adjust timer increment with deltaTime

            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }

            float translateChange = waveslice * bobAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);

            translateChange = totalAxes * translateChange;

            newPosition.y = midpoint + translateChange;
        }

        // Smooth transition using Lerp
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * 10f);
    }
}
