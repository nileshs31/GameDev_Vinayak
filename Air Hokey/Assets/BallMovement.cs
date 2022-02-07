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
        transform.Translate(Vector3.forward*Time.deltaTime*speed);
    }
}