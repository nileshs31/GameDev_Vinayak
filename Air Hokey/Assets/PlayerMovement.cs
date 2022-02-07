using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Moved){
            Vector2 pos;
            pos=Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
            transform.position=new Vector2(pos.x,pos.y+1f);
        }        
    }
}
