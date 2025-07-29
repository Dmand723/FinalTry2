using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryCrystalManager : MonoBehaviour
{
    [SerializeField] private GameObject shard1;
    [SerializeField] private GameObject shard2;
    [SerializeField] private GameObject shard3;
    bool[] shardsActive = new bool[3] { false, false, false };
    bool isActive = false;
    public void deactivateAll()
    {
        // Deactivate all crystal shards
        shard1.SetActive(false);
        shard2.SetActive(false);
        shard3.SetActive(false);
    }
    public void activateShard(int shardIndex)
    {
        if(!isActive)
        {
            //Activate the shards after first time grabbing a shard 
            isActive = true; // Set the flag to true when activating a shard
            PlayerController.Instance.addToInvetory(gameObject); // Add this GameObject to the player's inventory
        }
        // Activate the specified crystal shard based on the index
        switch (shardIndex)
        {
            case 1:
                shard1.SetActive(true);
                shardsActive[0] = true; // Mark shard1 as active
                break;
            case 2:
                shard2.SetActive(true);
                shardsActive[1] = true; // Mark shard2 as active
                break;
            case 3:
                shard3.SetActive(true);
                shardsActive[2] = true; // Mark shard3 as active
                break;
            default:
                Debug.LogWarning("Invalid shard index: " + shardIndex);
                break;
        }
    }
    public bool checkIfAllShardsActive()
    {
        // Check if all crystal shards are active
        return shardsActive[0] && shardsActive[1] && shardsActive[2];
    }
}
