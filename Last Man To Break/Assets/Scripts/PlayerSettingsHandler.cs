using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class PlayerSettingsHandler : MonoBehaviour
{

#region Public Variables
    public SO_Character[] characterList;
    public SO_PlayerSettingsPersist playerSettings;
    public int myCharacterIndex = 0;
#endregion

#region Private Variables
#endregion

    // Start is called before the first frame update
    void Start()
    {
        playerSettings.mySelectedCharacter = characterList[myCharacterIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#region Public Functions
    public void IncreaseCharacterIndex()
    {
        myCharacterIndex = (myCharacterIndex + 1) % characterList.Length;
        playerSettings.mySelectedCharacter = characterList[myCharacterIndex];
    }

    public void DecreaseCharacterIndex()
    {
        myCharacterIndex = Utilities.Modulus((myCharacterIndex -1), characterList.Length);
        playerSettings.mySelectedCharacter = characterList[myCharacterIndex];
    }

#endregion

#region Private Function

#endregion

}
