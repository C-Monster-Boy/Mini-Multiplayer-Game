using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class UI_GameSettingsUIHandler : MonoBehaviourPunCallbacks
{

#region UI Components

    [Header("Map UI")]
    public Button mapLeftButton;
    public Button mapRightButton;
    public Image mapImage;
    public Text mapName;

    [Header("Round Count UI")]
    public Button roundCountLeftButton;
    public Button roundCountRightButton;
    public Text roundCountText;

#endregion

#region Script References

    [Header("Script References")]
    public GameSettingsHandler gameSettingsHandler;

#endregion

    private void Start() 
    {
        InitializeGameSettingsUI();
    }


#region Public Functions
    public void DecreaseMapIndex()
    {
        gameSettingsHandler.DecrementSelectedMapIndex();
        ChangeGameSettingsButtonStatus(false);
    }

    public void IncreaseMapIndex()
    {
        gameSettingsHandler.IncrementSelectedMapIndex();
        ChangeGameSettingsButtonStatus(false);
    }

    public void IncreaseRoundCount()
    {
        gameSettingsHandler.IncrementRoundCountIndex();
        ChangeGameSettingsButtonStatus(false);
    }

    public void DecreaseRoundCount()
    {
        gameSettingsHandler.DecrementRoundCountIndex();
        ChangeGameSettingsButtonStatus(false);
    }

#endregion

#region PUN Callbacks

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        //Change Map 
        DisplayMap(propertiesThatChanged);
        DisplayRoundCount();

        if(PhotonNetwork.IsMasterClient)
        {
            ChangeGameSettingsButtonStatus(true);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            Initialize_MasterClient();
        }
    }

#endregion

#region Private Functions

     private void InitializeGameSettingsUI()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            Initialize_MasterClient();
        }
        else
        {
            Initialize_NonMasterClient();
        }
        DisplayMap();
        DisplayRoundCount();
    }

    private void Initialize_NonMasterClient()
    {
        mapLeftButton.gameObject.SetActive(false);
        mapRightButton.gameObject.SetActive(false);

        roundCountLeftButton.gameObject.SetActive(false);
        roundCountRightButton.gameObject.SetActive(false);
    }

    private void Initialize_MasterClient()
    {
        mapLeftButton.gameObject.SetActive(true);
        mapRightButton.gameObject.SetActive(true);

        roundCountLeftButton.gameObject.SetActive(true);
        roundCountRightButton.gameObject.SetActive(true);
    }

    private void ChangeGameSettingsButtonStatus(bool enabled)
    {
        mapLeftButton.interactable = enabled;
        mapRightButton.interactable = enabled;
        
        roundCountLeftButton.interactable = enabled;
        roundCountRightButton.interactable = enabled;

    }

    private void DisplayMap(Hashtable hash = null)
    {   
        if(hash == null)
        {
            hash = PhotonNetwork.CurrentRoom.CustomProperties;
        }

        int mapIndex = 0;

        if(hash.ContainsKey(HashtableConstants.MAP_INDEX))
        {
            mapIndex = (int)hash[HashtableConstants.MAP_INDEX];
        }

        SO_Map currMap = gameSettingsHandler.mapList[mapIndex];
        mapImage.sprite = currMap.mapImage;
        mapName.text = currMap.mapName;
    }

    private void DisplayRoundCount(Hashtable hash = null)
    {
        if(hash == null)
        {
            hash = PhotonNetwork.CurrentRoom.CustomProperties;
        }

        int roundIndex = 0;

        if(hash.ContainsKey(HashtableConstants.ROUND_COUNT_INDEX))
        {
            roundIndex = (int)hash[HashtableConstants.ROUND_COUNT_INDEX];
        }

        roundCountText.text = ""+ gameSettingsHandler.roundCountList[roundIndex];
    }

#endregion
}
