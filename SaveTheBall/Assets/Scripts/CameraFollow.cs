using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempos;

    void Start(){
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update(){
        if(player.position.x>=-4 && player.position.x<=4){
            tempos = transform.position;
            tempos.x=player.position.x;
            transform.position=tempos;
        }
    }
}