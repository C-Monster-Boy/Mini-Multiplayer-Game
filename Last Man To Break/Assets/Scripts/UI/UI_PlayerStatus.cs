using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStatus : MonoBehaviour
{
    [Header("UI Components")]
    public Image playerImage;
    public Text playerNick;

    [Header("Ready Button")]
    public GameObject readyButton;
    public GameObject notReadyButton;

    [Header("Ready Icon")]
    public GameObject readyIcon;
    public GameObject notReadyIcon;

    [Header("Master Icon")]
    public GameObject masterIcon;


    public void SetState_Ready()
    {
        notReadyButton.SetActive(true);
        readyButton.SetActive(false);

        readyIcon.SetActive(true);
        notReadyIcon.SetActive(false);
    }

    public void SetState_NotReady()
    {
        notReadyButton.SetActive(false);
        readyButton.SetActive(true);

        readyIcon.SetActive(false);
        notReadyIcon.SetActive(true);
    }

    public void DisableControlComponents()
    {
        readyButton.SetActive(false);
        notReadyButton.SetActive(false);
    }

    public void SetMasterIcon(bool enable)
    {
        masterIcon.SetActive(enable);
    }
}
