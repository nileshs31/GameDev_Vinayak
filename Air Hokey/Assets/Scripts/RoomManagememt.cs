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
    public GameObject Overlay, NewRoomText, JoinARoomUI, JoinRoomButton, CreateRoomUI, OldCreate, OldJoin, Startbutton;
    public InputField JoinRoomInpute;
    public int ui = 0; // 1 for create room 2 for join
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
        Startbutton.SetActive(false);
        Overlay.SetActive(false);
        Debug.Log("Connected to Master");
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    // public override void OnJoinedLobby()
    // {
    // 	Overlay.SetActive(false);
    // 	Debug.Log("Joined Lobby");
    // }
    public void StartButton()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("OnlineMP");
    }
    public void JoinARoom()
    {
        ui = 2;
        JoinARoomUI.SetActive(true);
        JoinRoomButton.SetActive(true);
        OldCreate.SetActive(false);
        OldJoin.SetActive(false);
    }
    public void JoinButton()
    {
        PhotonNetwork.JoinRoom(JoinRoomInpute.text);
        Debug.Log("Joinning");
        Startbutton.SetActive(true);
        Startbutton.GetComponentInChildren<Text>().text = "Wait";
    }
    public void CreateButton()
    {
        ui = 1;
        newroom = RandomStringGenerator();
        PhotonNetwork.CreateRoom(newroom);
        Debug.Log(newroom);
        NewRoomText.SetActive(true);
        CreateRoomUI.SetActive(true);
        OldCreate.SetActive(false);
        OldJoin.SetActive(false);
        Startbutton.SetActive(true);
        Startbutton.GetComponentInChildren<Text>().text = "Waiting";
        NewRoomText.GetComponent<TMPro.TextMeshProUGUI>().text += newroom;
    }
    // public override void OnJoinedRoom()
    // {
    //     if(PhotonNetwork.CurrentRoom.PlayerCount==2)
    //     CreateRoomUI.GetComponentInChildren<Text>().text="Both Players Connected";
    // }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient){
            CreateRoomUI.GetComponent<TMPro.TextMeshProUGUI>().text = "Both Players Connected";
            Startbutton.GetComponentInChildren<Text>().text = "Start";
        }
    }

    string RandomStringGenerator()
    {
        string characters = "abcdefghijklmnopqrstuvwzyz1234567890";
        string generated_string = "";

        for (int i = 0; i < 5; i++)
            generated_string += characters[Random.Range(0, 35)];

        return generated_string;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (ui == 2)
            {
                ui = 0;
                JoinARoomUI.SetActive(false);
                JoinRoomButton.SetActive(false);
                OldCreate.SetActive(true);
                OldJoin.SetActive(true);
                Startbutton.SetActive(false);
            }
            else if (ui == 1)
            {
                ui = 0;
                PhotonNetwork.LeaveRoom();
                NewRoomText.SetActive(false);
                CreateRoomUI.SetActive(false);
                OldCreate.SetActive(true);
                OldJoin.SetActive(true);
                Startbutton.SetActive(false);
                NewRoomText.GetComponent<TMPro.TextMeshProUGUI>().text = NewRoomText.GetComponent<TMPro.TextMeshProUGUI>().text.Substring(0,8);
            }
            else if(ui==0){
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("Menu");
            }
        }
    }
}