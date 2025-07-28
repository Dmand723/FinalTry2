using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class InteractablePickup : MonoBehaviour
{
    [SerializeField] public string interactionText = "Press E To Pickup";
    [SerializeField] public string itemName;
    [SerializeField] public Sprite iconImage;
    
}
