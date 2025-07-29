using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHandler : MonoBehaviour
{
    [SerializeField] public GameObject glowingPrefab; // The glowing crystal prefab to instantiate
    [SerializeField] public GameObject portalPrefab; // The portal prefab to instantiate
    [SerializeField] public GameObject normalCrystalPrefab; // The normal crystal prefab to instantiate
    [SerializeField] public string interactionText = "Press E To Grab Crystal"; // Text to display when interacting with the crystal
    public Pedistool activePedistool; // Reference to the active pedestal where the crystal is placed
}
