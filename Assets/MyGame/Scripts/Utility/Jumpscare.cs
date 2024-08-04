using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    public Animator jumpScareAnimator; // Reference to the Animator for the jump scare animation
    public AudioSource jumpScareAudio; // Reference to the AudioSource for the jump scare sound
    public float scareDuration = 2f; // Duration of the jump scare

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(TriggerJumpScare());
        }
    }

    private IEnumerator TriggerJumpScare()
    {
        if (jumpScareAnimator != null)
        {
            jumpScareAnimator.SetTrigger("playJumpScare");
        }

        if (jumpScareAudio != null)
        {
            jumpScareAudio.Play();
        }

        // Disable player movement or camera control if necessary
        // Example: PlayerController.instance.enabled = false;

        yield return new WaitForSeconds(scareDuration);

        // Re-enable player movement or camera control if disabled
        // Example: PlayerController.instance.enabled = true;

        // Optionally, you can destroy the jump scare object or disable it
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
