using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Pedistool : MonoBehaviour
{
    [SerializeField] Transform crystalPlacePoint; // The point where the crystal should be placed
    [SerializeField] Transform portalPlacePont; // The point where the portal should be placed
    GameObject activeCrystal; // Reference to the crystal GameObject
    GameObject activePortal; // Reference to the portal GameObject
    [SerializeField] public string interactionText = "Press E to place the crystal"; // Text to display when interacting with the pedestal

    public void setActiveCrystal(GameObject glowingCrystalPrefab,GameObject portal)
    {
        GameObject glowingCrystal =  Instantiate(glowingCrystalPrefab,crystalPlacePoint.position, Quaternion.identity); // Instantiate the crystal at the pedestal position
        activeCrystal = glowingCrystal; // Set the crystal GameObject
        glowingCrystal.transform.SetParent(transform); // Set the pedestal as the parent of the crystal
        glowingCrystal.transform.position = crystalPlacePoint.position; // Position the crystal at the pedestal
        glowingCrystal.transform.rotation = Quaternion.identity; // Reset rotation
        glowingCrystal.tag = "GlowingInteractable"; // Set the tag to GlowingInteractable
        glowingCrystal.GetComponent<CrystalHandler>().activePedistool = this; // Set the active pedestal reference in the crystal handler
        GameObject newPortal = Instantiate(portal, portalPlacePont.position, Quaternion.identity);
        activePortal = newPortal; // Set the portal GameObject  
        gameObject.tag = "InactivePedistool"; // Set the pedestal tag to InactivePedistool
    }
    public void setHomeCrystal(GameObject glowingCrystalPrefab, GameObject portal)
    {
       
       
        GameObject glowingCrystal = Instantiate(glowingCrystalPrefab, crystalPlacePoint.position, Quaternion.identity); // Instantiate the crystal at the pedestal position
        activeCrystal = glowingCrystal; // Set the crystal GameObject
        glowingCrystal.transform.SetParent(transform); // Set the pedestal as the parent of the crystal
        glowingCrystal.transform.position = crystalPlacePoint.position; // Position the crystal at the pedestal
        glowingCrystal.tag = "Untagged"; // Set the tag to Untagged
        glowingCrystal.GetComponent<CrystalHandler>().activePedistool = this; // Set the active pedestal reference in the crystal handler
        GameObject newPortal = Instantiate(portal, portalPlacePont.position, Quaternion.identity);
        activePortal = newPortal; // Set the portal GameObject  
        gameObject.tag = "InactivePedistool"; // Set the pedestal tag to InactivePedistool
        glowingCrystal.transform.Rotate(new Vector3(0, 90, 0));
        glowingCrystal.transform.position = new Vector3(glowingCrystal.transform.position.x,
            glowingCrystal.transform.position.y, glowingCrystal.transform.position.z - 0.2f); // Adjust the position slightly to the right
    }

    public void removeActiveCrystal()
    {
        if (activeCrystal != null)
        {
            Destroy(activeCrystal); // Destroy the active crystal GameObject
            activeCrystal = null; // Reset the reference to the active crystal
            Destroy(activePortal); // Destroy the active portal GameObject
        }
        gameObject.tag = "Pedistool"; // Reset the pedestal tag to Pedistool
    }
}


