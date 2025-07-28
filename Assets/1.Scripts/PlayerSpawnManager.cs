using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{

    PlayerController playerController;
    [SerializeField] Vector3 playerSpawnPos = new Vector3(0,1,0);
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerController.gameObject.transform.position = playerSpawnPos;
    }
}
