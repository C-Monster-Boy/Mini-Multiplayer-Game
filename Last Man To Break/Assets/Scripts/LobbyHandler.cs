using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class LobbyHandler : MonoBehaviourPunCallbacks
{

#region Private Variables
    private const int MAX_PLAYERS_IN_A_ROOM = 2;
    private const int ROOM_NMBER_TRY_LIMIT = 21;
    private int roomNumberTry = 0;
#endregion

#region Public Variables

    public LobbyStatus lobbyStatus = LobbyStatus.Idle;

#endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

#region Public Functions

    public void JoinRoom(string roomName)
    {
        lobbyStatus = LobbyStatus.Joining;
        PhotonNetwork.JoinRoom(roomName);
    }

    public void CreateRoom()
    {
        lobbyStatus = LobbyStatus.Joining;
        PhotonNetwork.CreateRoom($"Room {roomNumberTry}", new RoomOptions { MaxPlayers = MAX_PLAYERS_IN_A_ROOM});
    }

#endregion

#region PUN Callbacks

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError($"OnDisconnected() was called by pun {cause}");

        PhotonNetwork.LoadLevel("MainMenu");
    }

    public override void OnCreatedRoom()
    {
        roomNumberTry = 0;
        lobbyStatus = LobbyStatus.Connected;

        //TODO: Load New Scene
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if(roomNumberTry < ROOM_NMBER_TRY_LIMIT)
        {
            roomNumberTry++;
            CreateRoom();
        }
        else
        {
            roomNumberTry = 0;
            lobbyStatus = LobbyStatus.Failed;
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyStatus = LobbyStatus.Connected;
        //TOOD: Load New Scene
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        lobbyStatus = LobbyStatus.Failed;
    }

#endregion

#region Private Functions

#endregion

}


public enum LobbyStatus
{
    Idle, Joining, Failed, Connected
}
