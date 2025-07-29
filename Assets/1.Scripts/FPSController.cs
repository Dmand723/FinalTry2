using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class FPSController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81F;

    [Header("Look Sensitivity")]
    [SerializeField] private float lookSensitivity = 2.0f;
    [SerializeField] private float maxLookAngle = 80.0f;

    [Header("Debug Options")]
    [SerializeField] private bool debug = false;

    private CharacterController characterController;
    private Camera mainCamera;
    [SerializeField] private PlayerInputManager inputHandler;
    private Vector3 curMovement;
    private float verticalRotation = 0.0f;

    private bool canMove = true;

    public static FPSController Instance { get; private set; }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = GameObject.FindWithTag("InputManager").GetComponent<PlayerInputManager>();

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

    private void Update()
    {
        if (canMove)
        {
            HandleMove();
            HandleRotation();
        }
        drawInteractRay();
    }
    private void HandleRotation()
    {
        float mouseXRotation = inputHandler.LookInput.x * lookSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= inputHandler.LookInput.y * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);

        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void HandleMove()
    {
        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);

        Vector3 inputDir = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDir = transform.TransformDirection(inputDir);
        worldDir.Normalize();

        curMovement.x = worldDir.x * speed;
        curMovement.z = worldDir.z * speed;

        HandleJump();
        characterController.Move(curMovement * Time.deltaTime);

    }

    private void HandleJump()
    {
        if (characterController.isGrounded)
        {
            curMovement.y = -0.5f;

            if (inputHandler.JumpTriggered)
            {
                curMovement.y = jumpForce;
            }
        }
        else
        {
            curMovement.y -= gravity * Time.deltaTime;
        }
    }
    public void disableMovement()
    {
        canMove = false;
    }
    public void enableMovement()
    {
        canMove = true;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main;
    }
    void drawInteractRay()
    {
        if (debug)
        {
            Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 4f, Color.red);
        }
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 4f))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                // Show interaction prompt
                string textToShow = hit.collider.GetComponent
                    <InteractablePickup>().interactionText;
                CanvasHandler.Instance.setInteractText(textToShow);
                GameObject obj = hit.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MoveObjectToInventorySpace(obj);
                    PlayerController.Instance.addToInvetory(obj);
                }
            }
            else if (hit.collider.CompareTag("GlowingInteractable"))
            {
                GameObject glowingCrystal = hit.collider.gameObject;
                string textToShow = glowingCrystal.GetComponent<CrystalHandler>().
                    interactionText;
                CanvasHandler.Instance.setInteractText(textToShow);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Trigger interaction with the glowing interactable
                    GameObject obj = GameObject.Instantiate(glowingCrystal.GetComponent<CrystalHandler>()
                        .normalCrystalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    glowingCrystal.GetComponent<CrystalHandler>().activePedistool.removeActiveCrystal();
                    obj.GetComponent<CrystalHandler>().activePedistool = null;
                    MoveObjectToInventorySpace(obj);
                    PlayerController.Instance.addToInvetory(obj);
                }
            }
            else if (hit.collider.CompareTag("ShardInteractable"))
            {
                string textToShow = hit.collider.GetComponent<MainShardManager>().interactionText;
                CanvasHandler.Instance.setInteractText(textToShow);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Trigger interaction with the shard
                    int shardIndex = hit.collider.GetComponent<MainShardManager>().shardIndex;
                    PlayerController.Instance.activateShard(shardIndex);
                    Destroy(hit.collider.gameObject);
                }
            }
            else if (hit.collider.CompareTag("DigSpot") &&
                PlayerController.Instance.returnActiveInventoryItemName()
                == "Shovel")
            {
                string textToShow = hit.collider.GetComponent<DigSpot>().interactionText;
                CanvasHandler.Instance.setInteractText(textToShow);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Trigger digging action
                    hit.collider.GetComponent<DigSpot>().Dig();
                }
            }
            else if (hit.collider.CompareTag("Pedistool") &&
                PlayerController.Instance.returnActiveInventoryItemName().
                EndsWith("Crystal"))
            {
                string textToShow = hit.collider.GetComponent<Pedistool>().
                    interactionText;
                CanvasHandler.Instance.setInteractText(textToShow);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Place the crystal on the pedestal
                    GameObject activeItem = PlayerController.Instance.
                        getActiveInventoryItem();
                    Debug.Log(activeItem);

                    hit.collider.GetComponent<Pedistool>().setActiveCrystal(
                        activeItem.
                        GetComponent<CrystalHandler>().glowingPrefab, activeItem.
                        GetComponent<CrystalHandler>().portalPrefab);
                    Debug.Log(activeItem);
                    CanvasHandler.Instance.clearHotbarSlotIcon(
                        Array.IndexOf(PlayerController.Instance.getInventory(),
                        activeItem));
                    // Remove the crystal from the inventory
                    PlayerController.Instance.removeFromInventory(activeItem);
                    Destroy(activeItem, 0.1f); // Destroy the crystal after placing it   
                }
            }
            else if (hit.collider.CompareTag("Pedistool") &&
                PlayerController.Instance.returnActiveInventoryItemName() ==
                "CrystalToHome")
            {
                if (PlayerController.Instance.getActiveInventoryItem().GetComponent<InventoryCrystalManager>().checkIfAllShardsActive())
                {
                    // Show interaction prompt
                    CanvasHandler.Instance.setInteractText("Press E to place the Crystal and open the portl Home");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Place the portal crystal on the pedestal
                        GameObject activeItem = PlayerController.Instance.
                            getActiveInventoryItem();
                        hit.collider.GetComponent<Pedistool>().setHomeCrystal(
                            activeItem.
                            GetComponent<CrystalHandler>().glowingPrefab, activeItem.
                            GetComponent<CrystalHandler>().portalPrefab);
                        CanvasHandler.Instance.clearHotbarSlotIcon(
                            Array.IndexOf(PlayerController.Instance.getInventory(),
                            activeItem));
                        // Remove the crystal from the inventory
                        PlayerController.Instance.removeFromInventory(activeItem);
                        Destroy(activeItem, 0.1f); // Destroy the crystal after placing it   
                    }
                }
                else
                {
                    // Show interaction prompt
                    CanvasHandler.Instance.setInteractText("You Are Missing Shards");
                }


            }
            else
            {
                // Hide interaction prompt
                CanvasHandler.Instance.setInteractText("");
            }
        }
        else
        {
            // Hide interaction prompt if nothing is hit
            CanvasHandler.Instance.setInteractText("");
        }
    }

    private static void MoveObjectToInventorySpace(GameObject obj)
    {
        obj.transform.parent = PlayerController.
            Instance.inventoryTransform.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.tag = "Untagged";
        obj.transform.rotation = Quaternion.identity; // Reset rotation
    }
}
