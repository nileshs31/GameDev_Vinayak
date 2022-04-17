using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class RoomManagememt : MonoBehaviourPunCallbacks
{
    public string newroom;
    //public GameObject Player1,Player2;
    public static RoomManagememt Instance;
    public GameObject Overlay,NewRoomText,JoinARoomUI,JoinRoomButton,CreateRoomUI,OldCreate,OldJoin,Startbutton;
    public InputField JoinRoomInpute;
    void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Debug.Log("Connecting to Master");
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
        Overlay.SetActive(false);
		Debug.Log("Connected to Master");
		PhotonNetwork.AutomaticallySyncScene = true;

	}

	// public override void OnJoinedLobby()
	// {
	// 	Overlay.SetActive(false);
	// 	Debug.Log("Joined Lobby");
	// }
    public void StartButton(){
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if(PhotonNetwork.CurrentRoom.PlayerCount==2 && PhotonNetwork.IsMasterClient)
        PhotonNetwork.LoadLevel("OnlineMP");
    }
    public void JoinARoom(){
        JoinARoomUI.SetActive(true);
        JoinRoomButton.SetActive(true);
        OldCreate.SetActive(false);
        OldJoin.SetActive(false);
        Startbutton.GetComponentInChildren<Text>().text="Waiting";
    }
    public void JoinButton()
    {
        PhotonNetwork.JoinRoom(JoinRoomInpute.text);
        Debug.Log("Joinning");
        Startbutton.GetComponentInChildren<Text>().text="Wait";
    }
    public void CreateButton()
    {
        newroom=RandomStringGenerator();
        PhotonNetwork.CreateRoom(newroom);
        Debug.Log(newroom);
        NewRoomText.SetActive(true);
        CreateRoomUI.SetActive(true);
        OldCreate.SetActive(false);
        OldJoin.SetActive(false);
        NewRoomText.GetComponent<TMPro.TextMeshProUGUI>().text+=newroom;
    }
    // public override void OnJoinedRoom()
    // {
    //     if(PhotonNetwork.CurrentRoom.PlayerCount==2)
    //     CreateRoomUI.GetComponentInChildren<Text>().text="Both Players Connected";
    // }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
        CreateRoomUI.GetComponent<TMPro.TextMeshProUGUI>().text="Both Players Connected";
    }

    string RandomStringGenerator()
    {
        string characters = "abcdefghijklmnopqrstuvwzyz1234567890";
        string generated_string = "";

        for(int i = 0; i < 5; i++)
            generated_string += characters[Random.Range(0,35)];

        return generated_string;
    }

}