using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    [Header("References")]
    [SerializeField] VolumeProfile globalVolume; // Reference to the global volume for post-processing effects
    [SerializeField] Cubemap AtherSkyMap; // Reference to the Aether skybox cubemap
    [SerializeField] Cubemap IslandSkyMap; // Reference to the Island skybox cubemap
    private TeleportationManager teleportationManager; // Reference to the teleportation manager
    [SerializeField] GameObject AtherDirectionalLight; // Reference to the Aether directional light
    [SerializeField] GameObject MainLight; // Reference to the main light
    public string endGameResult;
    int endGameTimer = 10;//time in minuts
    float secCountdown = 0;
    private bool gameIsRunning = false;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager
        }
        DontDestroyOnLoad(AtherDirectionalLight); // Ensure Aether directional light persists
        DontDestroyOnLoad(MainLight); // Ensure main light persists
        AtherDirectionalLight.SetActive(false); // Ensure Aether directional light is off initially
        MainLight.SetActive(false); // Ensure main light is off initially
        teleportationManager = gameObject.GetComponent<TeleportationManager>();

    }
    private void Start()
    {
        DisablePlayer();
    }

    private void Update()
    {
        if (gameIsRunning)
        {
            secCountdown -= Time.deltaTime;// Decrease the end game timer by 1 second every 60 frames
            CanvasHandler.Instance.setTimerText(endGameTimer, secCountdown); // Update the timer text in the UI
            if (secCountdown <= 0f)
            {
                endGameTimer--; // Decrease the end game timer by 1 minute
                secCountdown = 60; // Reset the countdown to 60 seconds
            }
            if (endGameTimer <= -1)
            {
                GameLose(); // Trigger game lose if the timer reaches zero
            }
        }
    }

    private static void DisablePlayer()
    {
        PlayerInputManager.Instance.gameObject.SetActive(false);
        PlayerController.Instance.gameObject.SetActive(false);
        CanvasHandler.Instance.gameObject.SetActive(false);
    }

    public void LoadAetherScene()
    {
        FPSController.Instance.disableMovement();
        FPSController.Instance.Invoke("enableMovement", 3f); // Re-enable movement after 3 seconds
        CanvasHandler.Instance.activateAetherPortal();
        CanvasHandler.Instance.Invoke("deactivateAetherPortal", 3f); // Wait for 3 seconds before deactivating the portal
        
        teleportationManager.TeleportPlayer(1); // Teleport to Aether position

        ChangeSkybox(); // Change the skybox to Aether skybox
        setAtherLight(); // Set the Aether directional light
    }
    public void LoadIrisScene()
    {
        FPSController.Instance.disableMovement();
        FPSController.Instance.Invoke("enableMovement", 3f); // Re-enable movement after 3 seconds

        // Change this for the Iris portal
        CanvasHandler.Instance.activateAetherPortal();
        CanvasHandler.Instance.Invoke("deactivateAetherPortal", 3f); // Wait for 3 seconds before deactivating the portal
        
        teleportationManager.TeleportPlayer(3); // Teleport to Iris position

        ChangeSkybox(); // Change the skybox to Aether skybox
        revertLight(); // Revert to the main light
    }
    public void LoadMyceliaScene()
    {
        FPSController.Instance.disableMovement();
        FPSController.Instance.Invoke("enableMovement", 3f); // Re-enable movement after 3 seconds

        // Change the video clip to Mycelia portal video
        CanvasHandler.Instance.activateAetherPortal();
        CanvasHandler.Instance.Invoke("deactivateAetherPortal", 3f); // Wait for 3 seconds before deactivating the portal

        teleportationManager.TeleportPlayer(2); // Teleport to Mycelia position

        ChangeSkybox(); // Change the skybox to Aether skybox
        revertLight(); // Revert to the main light
    }
    public void LoadIslandScence()
    {
        FPSController.Instance.disableMovement();
        FPSController.Instance.Invoke("enableMovement", 3f); // Re-enable movement after 3 seconds

        // Change this for the island portal
        CanvasHandler.Instance.activateAetherPortal();
        CanvasHandler.Instance.Invoke("deactivateAetherPortal", 3f); // Wait for 3 seconds before deactivating the portal

        
        teleportationManager.TeleportPlayer(0); // Teleport to Island position

        revertSkybox(); // Revert to the Island skybox
        revertLight(); // Revert to the main light
    }
    public void LoadGame()
    {
        PlayerInputManager.Instance.gameObject.SetActive(true);
        CanvasHandler.Instance.gameObject.SetActive(true);
        PlayerController.Instance.gameObject.SetActive(true);

        SceneManager.LoadScene("Island");

        revertSkybox(); // Revert to the Island skybox
        Invoke(nameof(revertLight),0.5f); // Revert to the main light  
        gameIsRunning = true; // Set the game as running
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        
    }
    public void GameLose()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        gameIsRunning = false; // Stop the game from running
        DisablePlayer(); // Disable player controls and canvas
        endGameResult = "lose"; // Set the end game result to lose
        SceneManager.LoadScene("EndScene"); // Load the end game canvas scene
    }
    public void GameWin()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        gameIsRunning = false; // Stop the game from running
        DisablePlayer(); // Disable player controls and canvas
        endGameResult = "win"; // Set the end game result to win
        SceneManager.LoadScene("EndScene"); // Load the end game canvas scene
    }
    void ChangeSkybox()
    {
        if (globalVolume != null && globalVolume.TryGet(out HDRISky hdriSky))
        {
            if (hdriSky.hdriSky != null)
            {
                hdriSky.hdriSky.value = AtherSkyMap;
                hdriSky.hdriSky.overrideState = true;
            }
        }
    }
    void revertSkybox()
    {
        if (globalVolume != null && globalVolume.TryGet(out HDRISky hdriSky))
        {
            if (hdriSky.hdriSky != null)
            {
                hdriSky.hdriSky.value = IslandSkyMap;
                hdriSky.hdriSky.overrideState = true;
            }
        }
    }
    void revertLight()
    {
        AtherDirectionalLight.SetActive(false); // Disable the Aether directional light
        MainLight.SetActive(true); // Enable the main light
    }
    void setAtherLight()
    {
        MainLight.SetActive(false); // Disable the main light
        AtherDirectionalLight.SetActive(true); // Enable the Aether directional light
    }
}
