using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MatchmakingUIHandler : MonoBehaviour
{

private const string LOBBY_SCENE_NAME = "Lobby";

#region UI Components
[Header("UI Components")]
public Text roomNameText;

#endregion

#region Script References
[Header("Script References")]
public MatchmakingHandler matchmakingHandler;
#endregion

private void Start() 
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
