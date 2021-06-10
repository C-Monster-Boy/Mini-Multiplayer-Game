using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameSettingsHandler : MonoBehaviour
{
    [Header("Map Settings")]
    public SO_Map[] mapList;
    public int currentMapSelectedIndex = 0;
    

    [Header("Round count")]
    public int[] roundCountList = {3, 5, 7, 9};
    public int currentRoundCountIndex = 0;

#region Public Functions
    public void IncrementSelectedMapIndex()
    {
        currentMapSelectedIndex = (currentMapSelectedIndex + 1) % mapList.Length;
        UpdateCustomProperites_MapIndex();   
    }

    public void DecrementSelectedMapIndex()
    {
        currentMapSelectedIndex = modulus((currentMapSelectedIndex - 1), mapList.Length);
        UpdateCustomProperites_MapIndex();
        
    }

    public void IncrementRoundCountIndex()
    {
        currentRoundCountIndex = (currentRoundCountIndex + 1)% roundCountList.Length;
        UpdateCustomProperites_RoundCount();
    }

     public void DecrementRoundCountIndex()
    {
        currentRoundCountIndex = modulus((currentRoundCountIndex - 1) , roundCountList.Length);
        UpdateCustomProperites_RoundCount();
    }

#endregion

#region Private Functions

    private void UpdateCustomProperites_MapIndex()
    {
        Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        CheckAndAddKeyToHashtable<int>(ref hash, HashtableConstants.MAP_INDEX, currentMapSelectedIndex);

        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    private void UpdateCustomProperites_RoundCount()
    {
        Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        CheckAndAddKeyToHashtable<int>(ref hash, HashtableConstants.ROUND_COUNT_INDEX, currentRoundCountIndex);

        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    private void CheckAndAddKeyToHashtable<T>(ref Hashtable hash, string key, T value)
    {
        if(hash.ContainsKey(key))
        {
            hash[key] = value;
        }
        else
        {
            hash.Add(key, value);
        }
    }

    private int modulus(int a, int b) //for a%b
    {
        if(a<0)
            //return (Mathf.Abs(a * b) + a) % b;
            return ((a % b) + b) % b;
        
        return a%b;
    }

#endregion


}
