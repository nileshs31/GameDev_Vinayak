using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public static float speed=-10;
    void FixedUpdate(){
        gameObject.GetComponent<Rigidbody>().velocity=new Vector3(0f,0f,speed);
    }
    void Update(){
        if(transform.position.z<-8){
        Object.Destroy(gameObject,0f);
        GameManager.sc+=1;
        }
    }
}
