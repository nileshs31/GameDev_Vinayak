using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public GameObject Dead;
    private float speed=1500;
    void Update()
    {
        PlayerMove();
    }
        void PlayerMove(){
        gameObject.GetComponent<Rigidbody>().AddForce(speed*Input.GetAxisRaw("Horizontal")*Time.deltaTime,0,0,ForceMode.Acceleration);
    }
        void OnCollisionEnter(Collision Hit){
        if(Hit.collider.tag=="Obstacle"){
        Time.timeScale=0;
        Dead.SetActive(true);
        }
        }
}