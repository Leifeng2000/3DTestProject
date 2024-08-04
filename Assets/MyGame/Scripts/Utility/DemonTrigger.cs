using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DemonTrigger : MonoBehaviour
{
    public GameObject monster;
    public float delayBeforeChase = 2.8f;
    public AudioSource bgmAudioSource;

    private AIPath aiPath;
    private AudioSource audioSource;

    void Start()
    {
        // Get the AIPath component from the monster
        if (monster != null)
        {
            aiPath = monster.GetComponent<AIPath>();
            if (aiPath != null)
            {
                aiPath.enabled = false; // Ensure AIPath is initially disabled
            }
            monster.SetActive(false); // Ensure Monster is initially disabled
        }

        // Get the AudioSource component from the current GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (monster != null)
            {
                monster.SetActive(true); // Enable the Monster GameObject
                StartCoroutine(EnableAIPathAfterDelay()); // Start coroutine to enable AIPath after delay
            }

            if (bgmAudioSource != null)
            {
                bgmAudioSource.Stop();
            }

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    IEnumerator EnableAIPathAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeChase);

        if (aiPath != null)
        {
            aiPath.enabled = true; // Enable the AIPath script after the delay
        }
    }
}
