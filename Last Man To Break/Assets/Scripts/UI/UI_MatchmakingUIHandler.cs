using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
public class UI_MatchmakingUIHandler : MonoBehaviourPunCallbacks
{
    private const string LOBBY_SCENE_NAME = "Lobby";

    [Header("UI Components")]
    public Text roomNameText;
    public Button playButton;


    [Header("Script Reference")]
    public MatchmakingHandler matchmakingHandler;
    public PlayerJoinHandler playerJoinHandler;
    public GameSettingsHandler gameSettingsHandler;

    // Start is called before the first frame update
    void Start()
    {
        SetRoomName();
        SetPlayButton();
    }

    private void Update() 
    {
        if(PhotonNetwork.IsMasterClient)
        {
            playButton.interactable = playerJoinHandler.AreAllPlayersInReadyState() && PhotonNetwork.IsConnected;
        }
    }

#region Public Functions

    public void GoBackToLobby()
    {
        matchmakingHandler.LeaveRoom();
        
    }

    public void StartGame()
    {   
        //Start game when lobby is full
        //Scene loaded is based on settings
        //Player loaded is based on settings
        matchmakingHandler.LoadScene(gameSettingsHandler.mapList[gameSettingsHandler.currentMapSelectedIndex].sceneToLoadName);
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }

#endregion

#region PUN Callbacks

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
       SetPlayButton();
    }

    public override void OnLeftRoom()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene(LOBBY_SCENE_NAME);
       // matchmakingHandler.LoadScene(LOBBY_SCENE_NAME);
    }

#endregion

#region Private Functions

    private void SetRoomName()
    {
        roomNameText.text = matchmakingHandler.GetRoomName();
    }

    private void SetPlayButton()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            playButton.gameObject.SetActive(true);
        }
        else
        {
            playButton.gameObject.SetActive(false);
        }
    }

#endregion
}
