using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private float screen=Screen.width;
    public GameObject Dead;
    private int input;
    //private float speed=1500;

    void FixedUpdate()
    {
        PlayerMove();
        //TouchMove();
    }
    
    void PlayerMove(){
        //gameObject.GetComponent<Rigidbody>().AddForce(speed*Input.GetAxisRaw("Horizontal")*Time.deltaTime,0,0,ForceMode.Acceleration);
        if(input==-1){
            if(transform.position.x>-1){
                transform.position-=new Vector3(1,0,0);                                
            }  
            input=0;          
        }
        if(input==1){
            if(transform.position.x<1){
                transform.position+=new Vector3(1,0,0);                                
            }       
            input=0;     
        }
    }
    void OnCollisionEnter(Collision Hit){
        if(Hit.collider.tag=="Obstacle"){
        Time.timeScale=0;
        Dead.SetActive(true);
        }
    }
    /*void TouchMove(){
        if(Input.touchCount>0){
        Touch touch=Input.GetTouch(0);
        print(touch.position.x);
        if(touch.position.x>screen/2)
        gameObject.GetComponent<Rigidbody>().AddForce(speed*Time.deltaTime,0,0,ForceMode.Acceleration);
        else
        gameObject.GetComponent<Rigidbody>().AddForce(speed*Time.deltaTime,0,0,ForceMode.Acceleration);
        }
    }*/
    public void left(){
        input=-1;
    }
    public void right(){
        input=1;
    }
}