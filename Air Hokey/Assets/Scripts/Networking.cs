using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Networking : MonoBehaviourPunCallbacks
{
    //public GameObject Player1,Player2;
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene=true;
        //PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        PhotonNetwork.JoinLobby();
        //PhotonNetwork.JoinOrCreateRoom("MyRoom", new Photon.Realtime.RoomOptions(), new Photon.Realtime.TypedLobby("MyRoom", Photon.Realtime.LobbyType.Default));
    }
    public void StartOnline()
    {
        SceneManager.LoadScene("Lobby");
    }
}
