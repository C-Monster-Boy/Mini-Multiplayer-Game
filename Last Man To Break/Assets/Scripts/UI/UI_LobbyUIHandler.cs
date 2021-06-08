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
    public Button greenJoinButton;

    [Header("Message UI")]
    public GameObject creatingMessage;
    public GameObject joiningMessage;

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
           ShowNonInteractableMessage();
           EnaleJoinButtonForNonEmptyRoomName();
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
        if(joinGameInput.text.Length > 0)
        {
            lobbyHandler.JoinRoom(joinGameInput.text);
        }
        
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

    private void EnaleJoinButtonForNonEmptyRoomName()
    {
        bool canJoinGame = joinGameInput.text.Length > 0 && !joinGameDisablePanel.activeSelf;
        greenJoinButton.interactable = canJoinGame;
        
    }

    private void ShowNonInteractableMessage()
    {   

        bool joiningMode = lobbyHandler.lobbyStatus == LobbyStatus.Joining;
        bool creatingMode = lobbyHandler.lobbyStatus == LobbyStatus.Creating;
        bool isJoinMessageActive = joiningMessage.activeSelf;
        bool isCreateMessageActive = creatingMessage.activeSelf;

        if( joiningMode && !isJoinMessageActive )
        {
            DisableAllMessageObjects();
            joiningMessage.SetActive(true);
        }
        else if(creatingMode && !isCreateMessageActive )
        {
            DisableAllMessageObjects();
            creatingMessage.SetActive(true);
        }
        else if((!joiningMode && !creatingMode) && (isJoinMessageActive || isCreateMessageActive))
        {
            DisableAllMessageObjects();
        }
    }

    private void DisableAllMessageObjects()
    {
        joiningMessage.SetActive(false);
        creatingMessage.SetActive(false);
    }

#endregion
}
