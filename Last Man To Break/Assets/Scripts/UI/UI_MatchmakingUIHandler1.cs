using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_MatchmakingUIHandler1 : MonoBehaviour
{
     private const string LOBBY_SCENE_NAME = "Lobby";


    [Header("UI Components")]
    public Text roomNameText;


    [Header("Script Reference")]
    public MatchmakingHandler matchmakingHandler;

    // Start is called before the first frame update
    void Start()
    {
        SetRoomName();
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

#endregion
}
