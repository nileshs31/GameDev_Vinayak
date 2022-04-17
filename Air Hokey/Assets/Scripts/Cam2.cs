using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Cam2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.IsMasterClient)
        transform.rotation = Quaternion.Euler(0,0,180);      
    }
}
