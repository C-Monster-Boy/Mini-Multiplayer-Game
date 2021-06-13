using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class Temp_GameSceneTest : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    [Header("Bounds")]
    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    // Start is called before the first frame update
    void Start()
    {
         SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMatchmaking()
    {
        //Destroy game object
        PhotonNetwork.LoadLevel("Matchmaking");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }

    //PUN Callbacks
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
    }

    //Private 
    private void SpawnPlayer()
    {
        Vector2 randomPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
    }

}


