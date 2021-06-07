using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class Launcher : MonoBehaviourPunCallbacks
{
#region Private Variables

    private string gameVersion = "1";

    [Tooltip("Set the maximun players able to enter a room")]
    [SerializeField]
    private byte maxPlayersInRoom = 4;
#endregion

#region Public Variables
   public ConnectionState connectionState = ConnectionState.Disconnected;
#endregion

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            connectionState = ConnectionState.Connected;
        }
        else
        {
            Connect();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#region PUBLIC FUNCTIONS

    public void Connect()
    {
        if(!PhotonNetwork.IsConnected)
        {
            connectionState = ConnectionState.Connecting;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;   
        }
    }


#endregion

#region PUN CALLBACKS
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        //PhotonNetwork.JoinRandomRoom();

        connectionState = ConnectionState.Connected;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError($"OnDisconnected() was called by pun {cause}");

        connectionState = ConnectionState.Disconnected;

        MessageBus.Instance.AddMessageToQueue(MessageType.Disconnected);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersInRoom});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

#endregion

#region Private Functions


#endregion

}


public enum ConnectionState
{
    Connected, Disconnected, Connecting
}
