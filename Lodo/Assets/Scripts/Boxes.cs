using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    Vector2 ludosize,ludo;
    //Boundaries and Mid Point
    [System.NonSerialized]
    public static Vector2[] StartPoint;
    //To start at after outing from home
    [System.NonSerialized]
    public static Vector2 turnLeftUp,turnRightUp,turnLeftDown,turnRightDown;
    //here need to change the path and rotation
    [System.NonSerialized]
    public static Vector2 leftupPos,rightupPos,rightdownPos,leftdownPos;
    //need to go to this location
    [System.NonSerialized]
    public static Vector2[] GoIN;
    //To Go Final Walk
    public static int unit; // Smallest block size
    void Start()
    {
        StartPoint=new Vector2[4];
        GoIN=new Vector2[4];
        ludo=gameObject.transform.position;
        ludosize=GetComponent<BoxCollider2D>().bounds.size;
        unit=(int)Mathf.Round(ludosize.x/ludosize.y);
        //Debug.Log(ludosize);
        StartPoint[0]=new Vector2(ludo.x-unit,ludo.y-(unit)*6);
        StartPoint[3]=new Vector2(ludo.x+(unit)*6,ludo.y-unit);
        StartPoint[2]=new Vector2(ludo.x+unit,ludo.y+(unit)*6);
        StartPoint[1]=new Vector2(ludo.x-(unit)*6,ludo.y+unit);

        turnLeftUp=new Vector2(ludo.x-unit,ludo.y-(unit)*2);
        rightupPos=new Vector2(ludo.x-unit,ludo.y+(unit)*2);
        turnRightUp=new Vector2(ludo.x-(unit)*2,ludo.y+unit);
        rightdownPos=new Vector2(ludo.x+(unit)*2,ludo.y+unit);
        turnRightDown=new Vector2(ludo.x+(unit),ludo.y+(unit)*2);
        leftdownPos=new Vector2(ludo.x+(unit),ludo.y-(unit)*2);
        turnLeftDown=new Vector2(ludo.x+(unit)*2,ludo.y-(unit));
        rightupPos=new Vector2(ludo.x-(unit)*2,ludo.y-(unit));

        GoIN[0]=new Vector2(ludo.x,ludo.y-(unit)*6);
        GoIN[1]=new Vector2(ludo.x-(unit)*6,ludo.y);
        GoIN[2]=new Vector2(ludo.x,ludo.y+(unit)*6);
        GoIN[3]=new Vector2(ludo.x+(unit)*6,ludo.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
