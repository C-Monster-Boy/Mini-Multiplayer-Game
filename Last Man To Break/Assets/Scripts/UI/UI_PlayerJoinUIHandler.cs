using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

public class UI_PlayerJoinUIHandler : MonoBehaviour
{
    public UI_PlayerStatus[] statusPool;

    public PlayerJoinHandler playerJoinHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerStatusUI(playerJoinHandler.playerReadyList);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerJoinHandler.upadteUI)
        {
            UpdatePlayerStatusUI(playerJoinHandler.playerReadyList);

            playerJoinHandler.upadteUI = false;

        }
    }

    public void SetState_NotReady()
    {
        playerJoinHandler.SetReadyState(false);
    }

    public void SetState_Ready()
    {
        playerJoinHandler.SetReadyState(true);
    }

    public void UpdatePlayerStatusUI(PlayerReadyList list)
    {
        Debug.Log("Updating UI");
        ResetObjectPool();

        List<PlayerReadyStatus> playerReadyStatuses = list.GetPlayerReadyList();
        int index = 0;
        foreach(PlayerReadyStatus p in playerReadyStatuses )
        {
            statusPool[index].gameObject.SetActive(true);
            UI_PlayerStatus playerStatusUI = statusPool[index];
            
            //Set ready state
            SetReadyState(p, playerStatusUI);

            //Disable control for other players
            ManagePlayerStatusControlElements(p, playerStatusUI);

            //Mark Master Client
            MarkMaster(p, playerStatusUI);

            AssignFunctionToButton(playerStatusUI);

            index++;
        }
    }

#region Private Functions

    private void SetReadyState(PlayerReadyStatus playerReadyStatus, UI_PlayerStatus playerStatus)
    {
        if(playerReadyStatus.isReady)
        {
            playerStatus.SetState_Ready();
        }
        else
        {
                playerStatus.SetState_NotReady();
        }
    }

    private void ManagePlayerStatusControlElements(PlayerReadyStatus playerReadyStatus, UI_PlayerStatus playerStatus)
    {
        if(playerReadyStatus.playerActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            playerStatus.DisableControlComponents();
        }
    }

    private void MarkMaster(PlayerReadyStatus playerReadyStatus, UI_PlayerStatus playerStatus)
    {
        if(playerReadyStatus.playerActorNumber ==  PhotonNetwork.MasterClient.ActorNumber)
        {
            playerStatus.SetMasterIcon(true);
        }
        else
        {
            playerStatus.SetMasterIcon(false);  
        }
    }

    private void AssignFunctionToButton(UI_PlayerStatus playerStatus)
    {
        //Ready Button
        playerStatus.readyButton.GetComponent<Button>().onClick.AddListener(delegate {playerJoinHandler.SetReadyState(true);});

        //Not Ready Button
        playerStatus.notReadyButton.GetComponent<Button>().onClick.AddListener(delegate {playerJoinHandler.SetReadyState(false);});
    }

    private void ResetObjectPool()
    {
        foreach(UI_PlayerStatus item in statusPool)
        {
            item.readyButton.GetComponent<Button>().onClick.RemoveAllListeners();
            item.notReadyButton.GetComponent<Button>().onClick.RemoveAllListeners();

            item.gameObject.SetActive(false);
        }
    }


#endregion


}
