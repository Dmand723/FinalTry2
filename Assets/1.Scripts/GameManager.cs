using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] VolumeProfile globalVolume; // Reference to the global volume for post-processing effects
    [SerializeField] Cubemap AtherSkyMap; // Reference to the Aether skybox cubemap
    [SerializeField] Cubemap IslandSkyMap; // Reference to the Island skybox cubemap
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
        revertSkybox(); // Revert to the Island skybox on startup
    }
    private void Start()
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
        SceneManager.LoadScene("Aether");
        ChangeSkybox(); // Change the skybox to Aether skybox
    }
    public void LoadIrisScene()
    {
        FPSController.Instance.disableMovement();
        FPSController.Instance.Invoke("enableMovement", 3f); // Re-enable movement after 3 seconds

        // Change this for the Iris portal
        CanvasHandler.Instance.activateAetherPortal();
        CanvasHandler.Instance.Invoke("deactivateAetherPortal", 3f); // Wait for 3 seconds before deactivating the portal
        SceneManager.LoadScene("Iris");
        ChangeSkybox(); // Change the skybox to Aether skybox
    }
    public void LoadMyceliaScene()
    {
        FPSController.Instance.disableMovement();
        FPSController.Instance.Invoke("enableMovement", 3f); // Re-enable movement after 3 seconds

        // Change the video clip to Mycelia portal video
        CanvasHandler.Instance.activateAetherPortal();
        CanvasHandler.Instance.Invoke("deactivateAetherPortal", 3f); // Wait for 3 seconds before deactivating the portal

        SceneManager.LoadScene("Mycelia");
        ChangeSkybox(); // Change the skybox to Aether skybox
    }
    public void LoadIslandScence()
    {
        FPSController.Instance.disableMovement();
        FPSController.Instance.Invoke("enableMovement", 3f); // Re-enable movement after 3 seconds

        // Change this for the island portal
        CanvasHandler.Instance.activateAetherPortal();
        CanvasHandler.Instance.Invoke("deactivateAetherPortal", 3f); // Wait for 3 seconds before deactivating the portal

        SceneManager.LoadScene("Island");
        revertSkybox(); // Revert to the Island skybox
    }
    public void LoadGame()
    {
        PlayerInputManager.Instance.gameObject.SetActive(true);
        CanvasHandler.Instance.gameObject.SetActive(true);
        PlayerController.Instance.gameObject.SetActive(true);

        SceneManager.LoadScene("Island");
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
}
