using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; private set; }
    [SerializeField] private bool debug = false;
    [SerializeField] public GameObject inventoryTransform;
    [SerializeField] GameObject[] inventory;
    [SerializeField] GameObject activeInventroyItem;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AetherPortal"))
        {
            GameManager.Instance.LoadAetherScene();
        }
        if (other.CompareTag("IrisPortal"))
        {
            GameManager.Instance.LoadIrisScene();
        }
        if (other.CompareTag("MyceliaPortal"))
        {
            GameManager.Instance.LoadMyceliaScene();
        }
        if (other.CompareTag("IslandPortal"))
        {
            GameManager.Instance.LoadIslandScence();
        }
    }

    public void addToInvetory(GameObject item)
    {
        int iventroySize = inventory.Length;
        for (int i = 0; i < iventroySize; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                activeInventroyItem = item;
                switchIventoryItem();
                CanvasHandler.Instance.setHotbarSlotIcon(
                    Array.IndexOf(inventory, item),
                    item.GetComponent<InteractablePickup>());
                CanvasHandler.Instance.switchHotbarOutline(
                   Array.IndexOf(inventory, item));
                if (debug)
                    Debug.Log("Added " + item.name + " to inventory. Inventory size: "
                        + inventory.Length);
                return;// If there's an empty slot, add the item there
            }
        }
        GameObject[] newInventory = new GameObject[iventroySize + 1];
        for (int i = 0; i < iventroySize; i++)
        {
            newInventory[i] = inventory[i];
        }
        newInventory[iventroySize] = item;
        inventory = newInventory;
        activeInventroyItem = item;
        switchIventoryItem();
        CanvasHandler.Instance.setHotbarSlotIcon(newInventory.Length - 1,
            item.GetComponent<InteractablePickup>());
        CanvasHandler.Instance.switchHotbarOutline(newInventory.Length - 1);
        if (debug)
            Debug.Log("Added " + item.name + " to inventory. Inventory size: "
                + inventory.Length);
    }
    public void setActiveInventoryItem(string itemName)
    {
        if (itemName == "")
        {
            activeInventroyItem = null;
            return;
        }
        foreach (GameObject item in inventory)
        {
            if (item.GetComponent<InteractablePickup>().itemName == itemName)
            {
                activeInventroyItem = item;
                break;
            }
        }
    }
    public void switchIventoryItem()
    {
        foreach (GameObject item in inventory)
        {
            if (item == activeInventroyItem)
            {
                if(item != null) // Check if the item is not null before setting active
                    item.SetActive(true);

            }
            else
            {
                if (item != null) // Check if the item is not null before setting active
                    item.SetActive(false);
            }
        }
    }
    public string returnActiveInventoryItemName()
    {
        if (activeInventroyItem != null)
        {
            return activeInventroyItem.GetComponent<InteractablePickup>().itemName;
        }
        else
        {
            return "";
        }
    }
    public GameObject getActiveInventoryItem()
    {
        return activeInventroyItem;
    }
    public void removeFromInventory(GameObject item)
    {
        foreach (GameObject invItem in inventory)
        {
            if (invItem == item)
            { 
                inventory[Array.IndexOf(inventory, invItem)] = null; // Set the inventory slot to null

            }
        }
    }
    public GameObject[] getInventory()
    {
        return inventory;
    }
}

