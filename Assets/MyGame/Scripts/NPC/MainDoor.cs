using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainDoor : MonoBehaviour
{
    public string sceneToLoad;
    public float interactionDistance = 2f;
    private Transform player;

    void Start()
    {
        // Assuming the player has the tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadScene();
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
