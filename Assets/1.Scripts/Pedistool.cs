using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Pedistool : MonoBehaviour
{
    [SerializeField] Transform crystalPlacePoint; // The point where the crystal should be placed
    [SerializeField] Transform portalPlacePont; // The point where the portal should be placed
    GameObject activeCrystal; // Reference to the crystal GameObject
    [SerializeField] public string interactionText = "Press E to place the crystal"; // Text to display when interacting with the pedestal

    public void setActiveCrystal(GameObject crystal,GameObject portal)
    {
        GameObject glowingCrystal =  Instantiate(crystal,crystalPlacePoint.position, Quaternion.identity); // Instantiate the crystal at the pedestal position
        activeCrystal = glowingCrystal; // Set the crystal GameObject
        glowingCrystal.transform.SetParent(transform); // Set the pedestal as the parent of the crystal
        glowingCrystal.transform.position = crystalPlacePoint.position; // Position the crystal at the pedestal
        glowingCrystal.transform.rotation = Quaternion.identity; // Reset rotation
        glowingCrystal.tag = "Interactable"; // Set the tag to interactable
        GameObject newPortal = Instantiate(portal, portalPlacePont.position, Quaternion.identity);
    }
}


