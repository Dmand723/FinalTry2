using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using UnityEngine.Video;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] private float videoPlaybackSpeed = 3f; // Speed at which the video plays
    VideoPlayer videoPlayer;
    [SerializeField] VideoClip aetherVideoClip;
    [SerializeField] GameObject hotBar;
    [SerializeField] RectTransform[] hotBars;
    [SerializeField] RectTransform seletionOutline;
    [SerializeField] TextMeshProUGUI interactText;
    public static CanvasHandler Instance { get; private set; }

    private void Awake()
    {
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
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        videoPlayer.clip = aetherVideoClip; // Assign the video clip to the video player
        interactText.text = "";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (videoPlayer.isPlaying)
                activateAetherPortal();
            else
            {
                deactivateAetherPortal();
            }
        }
        if(Input.GetKey(KeyCode.Alpha1))
        {
            switchHotbarOutline(0);
            switchPlayerInvenItem(0);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            switchHotbarOutline(1);
            switchPlayerInvenItem(1);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            switchHotbarOutline(2);
            switchPlayerInvenItem(2);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            switchHotbarOutline(3);
            switchPlayerInvenItem(3);
        }
        

    }

    public void activateAetherPortal()
    {
        hotBar.SetActive(false); // Toggle hotbar visibility
        videoPlayer.enabled = true; // Enable video player to start rendering
        videoPlayer.frame = 0; // Reset video to the beginning
        videoPlayer.Play();
        videoPlayer.playbackSpeed = videoPlaybackSpeed;
    }

    public void deactivateAetherPortal()
    {
        hotBar.SetActive(true); // Toggle hotbar visibility
        videoPlayer.Pause();
        videoPlayer.enabled = false; // Disable video player to stop rendering
    }

    public void switchHotbarOutline(int hotBarInt)
    {
        seletionOutline.position = hotBars[hotBarInt].position;
    }
    public void setInteractText(string text)
    {
        interactText.text = text;
    }
    void switchPlayerInvenItem(int hotbarSlot)
    {
        PlayerController.Instance.switchIventoryItem();
        string activename = hotBars[hotbarSlot].GetComponent<HotbarSlot>().itemName;
        PlayerController.Instance.setActiveInventoryItem(activename);
    }
    public void setHotbarSlotIcon(int itemSlotInt, InteractablePickup interacableRef)
    {

        hotBars[itemSlotInt].GetComponent<HotbarSlot>().itemName =
            interacableRef.itemName;
        if (interacableRef.iconImage != null)
        {
            hotBars[itemSlotInt].GetComponent<HotbarSlot>()
                .setImageSprite(interacableRef.iconImage);

        }
        else
        {
            hotBars[itemSlotInt].GetComponent<HotbarSlot>().setAltName();
            Debug.LogWarning("Icon image is null for item: " + interacableRef.itemName);
            return;
        }
    }
    public void clearHotbarSlotIcon(int itemSlotInt)
    {
        hotBars[itemSlotInt].GetComponent<HotbarSlot>().emptySlot();
    }
}
