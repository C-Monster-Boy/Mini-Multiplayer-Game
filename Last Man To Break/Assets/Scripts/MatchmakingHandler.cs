using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MatchmakingHandler : MonoBehaviourPunCallbacks
{
    private void Awake() {
        if(PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient)
        {
            //PhotonNetwork.AutomaticallySyncScene = true;
        }

        PhotonNetwork.AutomaticallySyncScene = true;
    }

#region PUN Callbacks
    public override void OnDisconnected(DisconnectCause cause) 
    {
        MessageBus.Instance.AddMessageToQueue(MessageType.Disconnected);
    }

#endregion

#region  Public Functions

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        //Start game when lobby is full
        //Scene loaded is based on settings
        //Player loaded is based on settings
    }

    public string GetRoomName()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }

    public void LoadScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

#endregion

}
