using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

using Photon.Pun;
using Player = Photon.Realtime.Player;

[System.Serializable]
public class PlayerReadyList
{
    private List<PlayerReadyStatus> playerList;
        public PlayerReadyList()
    {
        playerList = new List<PlayerReadyStatus>();
    }
    public PlayerReadyList(Player[] players)
    {
        playerList = new List<PlayerReadyStatus>();
        
        foreach(Player p in players)
        {
            PlayerReadyStatus joinedStatus;
            joinedStatus.playerActorNumber = p.ActorNumber;
            joinedStatus.isReady = false;

            playerList.Add(joinedStatus);
        }
    }

    public static object Deserialize(byte[] data)
    {
        var result = new PlayerReadyList();

        BinaryFormatter bf = new BinaryFormatter();
        using(MemoryStream ms = new MemoryStream(data))
        {
            result = (PlayerReadyList)bf.Deserialize(ms);
        }

        return result;
    }

    public static byte[] Serialize(object customType)
    {
        var obj = (PlayerReadyList)customType;
        BinaryFormatter bf = new BinaryFormatter();
        using(MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

    }

    public List<PlayerReadyStatus> GetPlayerReadyList()
    {
        return playerList;
    }

    public void AddNewPlayer(Player newPlayer)
    {
        PlayerReadyStatus joinedStatus;
        joinedStatus.playerActorNumber = newPlayer.ActorNumber;
        joinedStatus.isReady = false;

        playerList.Add(joinedStatus);
    }

    public void RemovePlayer(Player player)
    {
        for(int i=0; i<playerList.Count; i++)
        {
            if(playerList[i].playerActorNumber == player.ActorNumber)
            {
                playerList.RemoveAt(i);
            }
        }
    }

    public void SetReadyStatus(Player player, bool isReady)
    {
       for(int i=0; i<playerList.Count; i++)
        {
            if(playerList[i].playerActorNumber == player.ActorNumber)
            {
                PlayerReadyStatus joinedStatus = playerList[i];
                joinedStatus.isReady = isReady;

                playerList[i] = joinedStatus;

                break;
            }
        }
    }

    public bool GetReadyStatus(Player player)
    {
        foreach(PlayerReadyStatus p in playerList)
        {
            if(player.ActorNumber == p.playerActorNumber)
            {
                return p.isReady;
            }
        }
        Debug.LogError("Player not found");
        return false;
    }
}
[System.Serializable]
public struct PlayerReadyStatus
{
    public int playerActorNumber;
    public bool isReady;
}
