using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionScript : MonoBehaviour
{
    [SerializeField] float deletionDelay = 0.0f; // Delay before deletion in seconds
    private void Awake()
    {
        Destroy(gameObject, deletionDelay); // Destroy this GameObject after the specified delay
    }
}
