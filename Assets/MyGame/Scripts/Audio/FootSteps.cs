using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public LayerMask groundLayer; // LayerMask for the ground layer

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider is part of the ground layer
        if ((groundLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            if (!audioSource.isPlaying) // Ensure the sound doesn't overlap
            {
                audioSource.Play();
            }
        }
    }
}
