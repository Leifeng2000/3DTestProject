using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEvent2 : MonoBehaviour
{
    public Animator enemyAnimator; // Reference to the enemy's Animator component
    public GameObject enemy;       // Reference to the enemy GameObject

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            enemyAnimator.SetTrigger("isTurn");
        }
    }
}
