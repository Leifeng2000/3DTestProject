using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;            // Reference to the player
    public float detectionRange = 10;  // Detection range
    public float fieldOfView = 180;    // Field of view angle

    public Patrol patrol;               // Reference to the AIPath script
    public AIDestinationSetter aiDestination; // Reference to the AIDestinationSetter script
    public Animator animator;

    public GameObject deathScreenUI;    // Reference to the DeathScreen UI
    public MouseMovement mouseMovement; // Reference to the MouseMovement script
    public PlayerController playerController; // Reference to the PlayerController script

    private void Update()
    {
        if (IsPlayerInSight())
        {
            patrol.enabled = false;
            aiDestination.enabled = true;
            animator.SetTrigger("isChasing");
        }
        else
        {
            patrol.enabled = true;
            aiDestination.enabled = false;
        }
    }

    private bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= detectionRange)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= fieldOfView / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, detectionRange))
                {
                    if (hit.transform == player)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw detection range sphere
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw field of view lines
        Vector3 fovLine1 = Quaternion.AngleAxis(fieldOfView / 2, transform.up) * transform.forward * detectionRange;
        Vector3 fovLine2 = Quaternion.AngleAxis(-fieldOfView / 2, transform.up) * transform.forward * detectionRange;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);

        // Draw ray to the player if in sight
        if (IsPlayerInSight())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            TriggerDeathScreen();
        }
    }

    private void TriggerDeathScreen()
    {
        // Activate the DeathScreen UI
        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(true);
        }

        // Disable player controls
        if (mouseMovement != null)
        {
            mouseMovement.enabled = false;
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }
        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
