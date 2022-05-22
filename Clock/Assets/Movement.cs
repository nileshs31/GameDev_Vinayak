using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Touch TheTouch;
    public GameObject One,Two,Three,Four,Five,Six,Seven,Eight,Nine,Ten,Eleven,Twelve,Current=null;
    private Vector2 mousePos,targetPos,originalPos;

    void Update()
    {
        if (Input.touchCount > 0){
            TheTouch = Input.GetTouch(0);
            mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
            if(TheTouch.phase==TouchPhase.Began){
                if(Mathf.Clamp(mousePos.x,-2,-1.34f)==mousePos.x && Mathf.Clamp(mousePos.y,-2.4f,-1.6f)==mousePos.y){
                Current=One;
                targetPos=new Vector2(0.75f,2.38f);
                }
                if(Mathf.Clamp(mousePos.x,-1.34f,-0.7f)==mousePos.x && Mathf.Clamp(mousePos.y,-2.4f,-1.6f)==mousePos.y){
                Current=Two;
                targetPos=new Vector2(1.28f,1.85f);
                }
                if(Mathf.Clamp(mousePos.x,-0.7f,0)==mousePos.x && Mathf.Clamp(mousePos.y,-2.4f,-1.6f)==mousePos.y){
                Current=Three;
                targetPos=new Vector2(1.5f,1.15f);
                }
                if(Mathf.Clamp(mousePos.x,0,0.65f)==mousePos.x && Mathf.Clamp(mousePos.y,-2.4f,-1.6f)==mousePos.y){
                Current=Four;
                targetPos=new Vector2(1.3f,0.48f);
                }
                if(Mathf.Clamp(mousePos.x,0.65f,1.3f)==mousePos.x && Mathf.Clamp(mousePos.y,-2.4f,-1.6f)==mousePos.y){
                Current=Five;
                targetPos=new Vector2(0.8f,-0.1f);
                }
                if(Mathf.Clamp(mousePos.x,1.36f,2)==mousePos.x && Mathf.Clamp(mousePos.y,-2.4f,-1.6f)==mousePos.y){
                Current=Six;
                targetPos=new Vector2(0f,-0.4f);
                }
                if(Mathf.Clamp(mousePos.x,-2,-1.34f)==mousePos.x && Mathf.Clamp(mousePos.y,-3.2f,-2.4f)==mousePos.y){
                Current=Seven;
                targetPos=new Vector2(-0.76f,-0.15f);
                }
                if(Mathf.Clamp(mousePos.x,-1.34f,-0.7f)==mousePos.x && Mathf.Clamp(mousePos.y,-3.2f,-2.4f)==mousePos.y){
                Current=Eight;
                targetPos=new Vector2(-1.3f,0.4f);
                }
                if(Mathf.Clamp(mousePos.x,-0.7f,0)==mousePos.x && Mathf.Clamp(mousePos.y,-3.2f,-2.4f)==mousePos.y){
                Current=Nine;
                targetPos=new Vector2(-1.45f,1.15f);
                }
                if(Mathf.Clamp(mousePos.x,0,0.65f)==mousePos.x && Mathf.Clamp(mousePos.y,-3.2f,-2.4f)==mousePos.y){
                Current=Ten;
                targetPos=new Vector2(-1.4f,1.85f);
                }
                if(Mathf.Clamp(mousePos.x,0.65f,1.36f)==mousePos.x && Mathf.Clamp(mousePos.y,-3.2f,-2.4f)==mousePos.y){
                Current=Eleven;
                targetPos=new Vector2(-0.82f,2.42f);
                }
                if(Mathf.Clamp(mousePos.x,1.36f,2)==mousePos.x && Mathf.Clamp(mousePos.y,-3.2f,-2.4f)==mousePos.y){
                Current=Twelve;
                targetPos=new Vector2(-0.15f,2.65f);
                }
                originalPos=Current.transform.position;
            }
            if(Current==null)
                return;
            Current.transform.position=mousePos;
            if(TheTouch.phase==TouchPhase.Ended){
                if(Vector2.Distance(Current.transform.position,targetPos)<0.8f)
                Current.transform.position=targetPos;
                else
                Current.transform.position=originalPos;
                Current=null;
            }
        }
    }
}