using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign in Inspector
    public GameObject panel; // Assign in Inspector
    public AudioClip scareSound; // Assign in Inspector
    private AudioSource audioSource;
    private bool playerDisabled = false;

    void Start()
    {
        audioSource = videoPlayer.GetComponent<AudioSource>();
        panel.SetActive(false); // Hide the panel initially
        videoPlayer.loopPointReached += OnVideoEnd; // Subscribe to the video end event
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true); // Show the panel
            videoPlayer.Play(); // Play the video
            audioSource.PlayOneShot(scareSound); // Play the scare sound

            // Optionally, disable player controls if you have a PlayerController script
            if (other.GetComponent<PlayerController>())
            {
                other.GetComponent<PlayerController>().enabled = false;
                playerDisabled = true;
            }
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        panel.SetActive(false); // Hide the panel

        // Optionally, re-enable player controls if they were disabled
        if (playerDisabled)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && player.GetComponent<PlayerController>())
            {
                player.GetComponent<PlayerController>().enabled = true;
                playerDisabled = false;
            }
        }
    }
}
