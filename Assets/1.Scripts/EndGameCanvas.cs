using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] Color WinColor;
    [SerializeField] Color LoseColor;
    [SerializeField] string WinText = "Game Over You Win!!!";
    [SerializeField] string LoseText = "Game Over You Lose!!!";
    TextMeshProUGUI endGameText;
    RawImage BG;

    private void Awake()
    {
        endGameText = GetComponentInChildren<TextMeshProUGUI>();
        BG = GetComponentInChildren<RawImage>();
        if (GameManager.Instance.endGameResult.ToLower() == "win")
        {
            endGameText.text = WinText;
            BG.color = WinColor;
        }
        else if(GameManager.Instance.endGameResult.ToLower() == "lose")
        {
            endGameText.text = LoseText;
            BG.color = LoseColor;
        }
    }
    public void OnExitBtnClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
