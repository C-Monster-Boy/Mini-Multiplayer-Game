using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class PlayerSettingsHandler : MonoBehaviour
{

#region Public Variables
    public SO_Character[] characterList;
    public int yourCharacterIndex = 0;
#endregion

#region Private Variables
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
    public void IncreaseCharacterIndex()
    {
        yourCharacterIndex = (yourCharacterIndex + 1) % characterList.Length;
    }

    public void DecreaseCharacterIndex()
    {
        yourCharacterIndex = Utilities.Modulus((yourCharacterIndex -1), characterList.Length);
    }

#endregion

#region Private Function

#endregion

}
