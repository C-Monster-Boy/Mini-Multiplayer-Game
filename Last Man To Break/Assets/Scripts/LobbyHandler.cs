using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyHandler : MonoBehaviourPunCallbacks
{

#region Private Variables
    private const int MAX_PLAYERS_IN_A_ROOM = 3;
    private const int ROOM_NMBER_TRY_LIMIT = 21;

    private const string MATCHMAKING_LEVEL_NAME = "Matchmaking";
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
        lobbyStatus = LobbyStatus.Creating;

        RoomOptions roomOptions  = new RoomOptions()
        {
            MaxPlayers =  MAX_PLAYERS_IN_A_ROOM
        };

        //Custom Room Properties
        Hashtable RoomCustomProps = new Hashtable();

        //Custom Props
        RoomCustomProps.Add(HashtableConstants.MAP_INDEX, 0); //map index has to be shared
        RoomCustomProps.Add(HashtableConstants.ROUND_COUNT_INDEX, 0); //roun count inex to be sharrd

        roomOptions.CustomRoomProperties = RoomCustomProps;


        PhotonNetwork.CreateRoom($"Room {roomNumberTry}", roomOptions);
    }

#endregion

#region PUN Callbacks

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError($"OnDisconnected() was called by pun {cause}");

        MessageBus.Instance.AddMessageToQueue(MessageType.Disconnected);
    }

    public override void OnCreatedRoom()
    {
        roomNumberTry = 0;
        lobbyStatus = LobbyStatus.Connected;
        
        Debug.Log("Room Created");

        LoadMatchmakingMenu();
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
            Debug.Log("Create room failed");
            MessageBus.Instance.AddMessageToQueue(MessageType.CreateRoomFailed);
            roomNumberTry = 0;
            lobbyStatus = LobbyStatus.Failed;
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        lobbyStatus = LobbyStatus.Connected;
        
        //PhotonNetwork.AutomaticallySyncScene = true;
        LoadMatchmakingMenu();
        
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        MessageBus.Instance.AddMessageToQueue(MessageType.JoinRoomFailed);
        Debug.Log("Join Room Failed");
        lobbyStatus = LobbyStatus.Failed;
    }

#endregion

#region Private Functions

    private void LoadMatchmakingMenu()
    {
        PhotonNetwork.LoadLevel(MATCHMAKING_LEVEL_NAME);
    }
#endregion

}


public enum LobbyStatus
{
    Idle, Joining, Creating, Failed, Connected
}
