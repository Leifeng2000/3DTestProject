using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour
{
    public Transform player;
    public AIPath chasePlayerScript;
    public GameObject deathScreenUI;
    public MouseMovement mouseMovement;
    public PlayerController playerController;
    public Animator animator;

    private Renderer mannequinRenderer;

    void Start()
    {
        mannequinRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (IsPlayerLookingAtMannequin())
        {
            chasePlayerScript.enabled = false;
            animator.SetTrigger("isIdle");
        }
        else
        {
            chasePlayerScript.enabled = true;
        }
    }

    bool IsPlayerLookingAtMannequin()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(planes, mannequinRenderer.bounds))
        {
            Vector3 directionToMannequin = mannequinRenderer.bounds.center - Camera.main.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, directionToMannequin, out hit))
            {
                if (hit.transform == transform)
                {
                    return true;
                }
            }
        }
        return false;
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
