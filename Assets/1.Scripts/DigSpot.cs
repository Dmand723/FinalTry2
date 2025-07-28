using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSpot : MonoBehaviour
{
    [SerializeField] public string interactionText = "Press E to dig here";
    [SerializeField] GameObject particalSystem;
    [SerializeField] GameObject itemToDig;
    public void Dig()
    {
        
        particalSystem.SetActive(false);
        if (itemToDig != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0,0.5f,0);
            GameObject item = Instantiate(itemToDig, spawnPosition, Quaternion.identity);
            item.tag = "Interactable";
            Destroy(gameObject);
        }
    }
}
