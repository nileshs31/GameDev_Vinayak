using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed=10;

    void Update(){
        FreeRom();
    }
    private void FreeRom(){
        gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(0f,speed);
    }
}