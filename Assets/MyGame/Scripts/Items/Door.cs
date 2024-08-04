using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator component found on this door.");
        }
        audioSource = GetComponent<AudioSource>();
        
    }

    public void OpenDoor()
    {
        if (animator != null)
        {
            animator.SetTrigger("isOpen");
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
