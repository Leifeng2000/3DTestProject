using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("LeiFeng/PlayerController")]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float sprintSpeed = 6.0f;
    public float gravity = -9.81f;
    public float interactionDistance = 3.0f;

    public AudioSource audioSource;
    public AudioClip walkClip;
    public AudioClip runClip;
    public float walkStepInterval = 1f;
    public float runStepInterval = 0.5f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float stepTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if the character is grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Determine movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Check if the player is sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        // Apply movement
        controller.Move(move * speed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        HandleFootsteps(move, isSprinting);

        // Check for interaction
        InteractWithObjects();
    }

    private void HandleFootsteps(Vector3 move, bool isSprinting)
    {
        if (isGrounded && move.magnitude > 0)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                // Play the appropriate footstep sound
                if (isSprinting)
                {
                    audioSource.PlayOneShot(runClip);
                    stepTimer = runStepInterval;
                }
                else
                {
                    audioSource.PlayOneShot(walkClip);
                    stepTimer = walkStepInterval;
                }
            }
        }
    }

    private void InteractWithObjects()
    {
        // Create a ray from the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Check if the ray hits a door within the interaction distance
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Door"))
            {
                // Check if the 'E' key is pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("E key pressed while looking at door: " + hit.collider.name);
                    // Get the Door component and trigger the open method
                    Door door = hit.collider.GetComponent<Door>();
                    if (door != null)
                    {
                        door.OpenDoor();
                    }
                }
            }
        }
    }
}
