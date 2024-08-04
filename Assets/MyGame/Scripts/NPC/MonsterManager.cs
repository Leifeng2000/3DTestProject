using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monsters;
    public float patrolDuration = 180f;

    private int currentMonsterIndex = -1; // Index of the currently active monster
    private bool isSwitching = false; // Flag to prevent multiple switches at the same time

    void Start()
    {
        if (monsters.Length == 0)
        {
            Debug.LogError("No monsters assigned to MonsterManager!");
            return;
        }

        // Disable all monsters initially
        foreach (GameObject monster in monsters)
        {
            monster.SetActive(false);
        }

        // Start the patrol rotation
        StartCoroutine(SwitchMonsterPatrol());
    }

    IEnumerator SwitchMonsterPatrol()
    {
        while (true)
        {
            if (!isSwitching)
            {
                isSwitching = true;

                // Select a random monster
                int nextMonsterIndex;
                do
                {
                    nextMonsterIndex = Random.Range(0, monsters.Length);
                } while (nextMonsterIndex == currentMonsterIndex);

                // Disable the current monster
                if (currentMonsterIndex != -1)
                {
                    monsters[currentMonsterIndex].SetActive(false);
                }

                // Enable the next monster
                currentMonsterIndex = nextMonsterIndex;
                monsters[currentMonsterIndex].SetActive(true);

                // Wait for the patrol duration before switching again
                yield return new WaitForSeconds(patrolDuration);

                isSwitching = false;
            }
        }
    }
}
