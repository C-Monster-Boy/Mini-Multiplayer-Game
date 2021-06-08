using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UI_LobbyUIHandler : MonoBehaviour
{
#region UI Components

    [Header("UI Components")]
    public Button joinGameButton;
    public Button createGameButton;
    public GameObject joinGameDisablePanel;
    public GameObject createGameDisablePanel;
    public InputField joinGameInput;

#endregion

#region Lobby Handler Reference

    [Header("Lobby Handler Reference")]
    public LobbyHandler lobbyHandler;

#endregion

    // Start is called before the first frame update
    void Start()
    {
        InitializeLobbyUI();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
#region Public Functions

    public void JoinGameMode()
    {
        joinGameButton.interactable = false;
        joinGameDisablePanel.SetActive(false);
        
        createGameButton.interactable = true;
        createGameDisablePanel.SetActive(true);
    }

    public void CreateGameMode()
    {
        joinGameButton.interactable = true;
        joinGameDisablePanel.SetActive(true);
        
        createGameButton.interactable = false;
        createGameDisablePanel.SetActive(false);
    }

    public void CreateRoom()
    {
        lobbyHandler.CreateRoom();
    }

    public void JoinRoom()
    {
        lobbyHandler.JoinRoom(joinGameInput.text);
    }

    public void GoToMainMenu()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }


#endregion

#region Private Function

    private void InitializeLobbyUI()
    {
        joinGameButton.interactable = true;
        joinGameDisablePanel.SetActive(true);
        
        createGameButton.interactable = true;
        createGameDisablePanel.SetActive(true);
    }

#endregion
}
