using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnPlayer : MonoBehaviour
{
    //PhotonView PV;
    GameObject Player1,Player2;
    void Awake(){
        //PV=gameObject.GetComponent<PhotonView>();
    }
    void Start() {
        //print(PhotonNetwork.IsMasterClient);
        if(PhotonNetwork.IsMasterClient){
            Player1=PhotonNetwork.Instantiate("Player-1",new Vector3(0,-15,0),Quaternion.identity);
            //PhotonNetwork.Instantiate("Ball",Vector3.zero,Quaternion.identity);
        }
        else{
            Player2=PhotonNetwork.Instantiate("Player-2",new Vector3(0,15,0),Quaternion.identity);
            //PhotonNetwork.Instantiate("Cam2",new Vector3(-0.33f,0,-10),Quaternion.Euler(0,0,180));
        }
    }

}
