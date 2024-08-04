using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public Text itemsCollectedText;
    public int totalItems;
    private int itemsCollected;

    public GameObject collectiblePrefab; // The prefab for the collectible items
    public Transform[] spawnPoints; // Array of spawn points

    private const int maxItemsToCollect = 25; // Maximum number of items to collect

    private void Awake()
    {
        // Ensure there is only one instance of ItemManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        itemsCollected = 0;
        UpdateUI();
        RespawnItem(); // Spawn the first item
    }

    public void CollectItem(CollectibleItem item)
    {
        itemsCollected++;
        UpdateUI();

        // Check if the maximum number of items has been collected
        if (itemsCollected < maxItemsToCollect)
        {
            RespawnItem();
        }
        else
        {
            Debug.Log("Maximum number of items collected.");
        }
    }

    private void UpdateUI()
    {
        itemsCollectedText.text = "Dolls collected: " + itemsCollected + "/" + totalItems;
    }

    private void RespawnItem()
    {
        // Pick a random spawn point
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Instantiate a new collectible item at the chosen spawn point
        Instantiate(collectiblePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
