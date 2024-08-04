using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public float xRotation;
    public float yRotation;
    public float topClamp = 90f;
    public float bottomClamp = -90f;
    public float zoomFOV = 30f; // Field of View when zoomed in
    public float normalFOV = 60f; // Normal Field of View
    public float zoomSpeed = 10f; // Speed of zooming in and out

    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = Camera.main;
        playerCamera.fieldOfView = normalFOV;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseMovement();
        HandleZoom();
    }

    private void HandleMouseMovement()
    {
        // Use raw mouse input for consistent sensitivity
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * 0.02f; // Adjust sensitivity scaling factor as needed
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * 0.02f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, bottomClamp, topClamp);
        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void HandleZoom()
    {
        if (Input.GetMouseButton(1)) // Right mouse button held down
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomSpeed * Time.deltaTime);
        }
        else
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, normalFOV, zoomSpeed * Time.deltaTime);
        }
    }
}
