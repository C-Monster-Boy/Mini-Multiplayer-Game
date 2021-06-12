using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PlayerJoinHandler : MonoBehaviourPunCallbacks
{

#region RPC Calls Constants

    private const string RPC_SYNC_READY_STATE = "SyncPlayerReadyList";
    private const string RPC_SET_READY_STATE = "SetReadyState";

#endregion

  public PlayerReadyList playerReadyList;

  public bool upadteUI = false;

    // Start is called before the first frame update
  public override void OnEnable()
  {
      base.OnEnable();
      ResetPlayerReadyList();
  }

#region Public Functions

  public void SetReadyState(bool isReady)
  {
      if(PhotonNetwork.IsMasterClient)
      {
          SetReadyState(PhotonNetwork.LocalPlayer, isReady);
          SyncStateIfMaster();
      }
      else
      {
          photonView.RPC(RPC_SET_READY_STATE, RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, isReady );
      }
      
      Debug.Log("Making RPC with isReady " + isReady);
  }

  public bool GetOwnReadyState()
  {
      return playerReadyList.GetReadyStatus(PhotonNetwork.LocalPlayer);
  }

#endregion

#region PUN Callbacks
  public override void OnPlayerEnteredRoom (Player newPlayer)
  {
      if(PhotonNetwork.IsMasterClient)
      {
          playerReadyList.AddNewPlayer(newPlayer);
      }

      SyncStateIfMaster();
      
  }

  public override void OnPlayerLeftRoom(Player otherPlayer)
  {
      if(PhotonNetwork.IsMasterClient)
      {
          playerReadyList.RemovePlayer(otherPlayer);
      }

      SyncStateIfMaster();

      Debug.Log("A Player left the room");
      
  }

  public override void OnMasterClientSwitched(Player newMasterClient)
  {   
      ResetPlayerReadyList();
      SyncStateIfMaster();
      Debug.Log("Master CLient changed");
  }

#endregion

#region RPC Calls
  [PunRPC]
  public void SyncPlayerReadyList(PlayerReadyList list)
  {
      playerReadyList = list;
      upadteUI = true;
  }

  //Only master client can call this
  [PunRPC]
  public void SetReadyState(Player player, bool isReady)
  {
      playerReadyList.SetReadyStatus(player, isReady);
      upadteUI = true;

      SyncStateIfMaster();
      
  }

#endregion

#region Private Functions

  private void ResetPlayerReadyList()
  {
      playerReadyList = new PlayerReadyList(PhotonNetwork.PlayerList);
  }

  private void SyncStateIfMaster()
  {
      if(PhotonNetwork.IsMasterClient)
      {
          photonView.RPC(RPC_SYNC_READY_STATE, RpcTarget.All, playerReadyList);
      }
  }

#endregion

}
