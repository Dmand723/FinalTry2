using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    [SerializeField] private int slotIndex; // Index of the hotbar slot
    [SerializeField] public string itemName; // Name of the item in the slot
    [SerializeField] TextMeshProUGUI altItemText; // Text component to display the item name
    [SerializeField] public GameObject iconImageObj; // Image component to display the item icon
    
    Image iconImage; // Reference to the Image component
    private void Awake()
    {
        emptySlot();
    }

    public void emptySlot()
    {
        iconImage = iconImageObj.GetComponent<Image>();
        iconImageObj.SetActive(false); // Initially hide the icon image
        altItemText.text = ""; // Initialize the alt item text
    }

    public void setAltName()
    {
        altItemText.text = itemName;
    }
    public void setImageSprite(Sprite sprite)
    {
        iconImage.sprite = sprite; // Set the sprite for the icon image
        iconImageObj.SetActive(true); // Show the icon image
    }
}
