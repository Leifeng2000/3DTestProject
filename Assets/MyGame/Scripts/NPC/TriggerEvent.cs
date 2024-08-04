using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public Animator enemyAnimator; // Reference to the enemy's Animator component
    public GameObject enemy;       // Reference to the enemy GameObject

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Set the "isHiding" trigger parameter on the Animator
            enemyAnimator.SetTrigger("isHiding");

            // Start a coroutine to deactivate the enemy after a short delay
            StartCoroutine(DeactivateEnemy());
        }
    }

    private IEnumerator DeactivateEnemy()
    {
        // Wait for a short period to allow the hiding animation to play
        yield return new WaitForSeconds(1f);

        // Deactivate the enemy GameObject
        enemy.SetActive(false);
    }
}
