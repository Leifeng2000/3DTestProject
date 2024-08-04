using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;

    public void Collect()
    {
        // Notify the ItemManager that this item has been collected
        ItemManager.Instance.CollectItem(this);
        // Destroy the item after it's collected
        Destroy(gameObject);
    }
}
