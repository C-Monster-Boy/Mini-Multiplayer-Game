using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
public class UI_MatchmakingUIHandler1 : MonoBehaviour
{
     private const string LOBBY_SCENE_NAME = "Lobby";


    [Header("UI Components")]
    public Text roomNameText;
    public Button playButton;


    [Header("Script Reference")]
    public MatchmakingHandler matchmakingHandler;
    public PlayerJoinHandler playerJoinHandler;

    // Start is called before the first frame update
    void Start()
    {
        SetRoomName();
        SetPlayButton();
    }

    private void Update() {
        if(PhotonNetwork.IsMasterClient)
        {
            playButton.interactable = playerJoinHandler.AreAllPlayersInReadyState();
        }
    }
#region Public Functions

    public void GoBackToLobby()
    {
        matchmakingHandler.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene(LOBBY_SCENE_NAME);
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
