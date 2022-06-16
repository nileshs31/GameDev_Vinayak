using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyRunner : MonoBehaviour
{
    private Vector2 bPos,mPos;
    public static float speed = -2;
    private int addscore;
    private bool move = false,notMove=false;
    private Rigidbody2D rb;
    private void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
        if (gameObject.name == "Red(Clone)")
            addscore = -10;
        else if (gameObject.name == "Green(Clone)")
            addscore = 30;
        else if (gameObject.name == "Yellow(Clone)")
            addscore = 20;
        else
            addscore = 10;
    }
    void Update()
    {
        if(Gamepaly.time<1)
        Destroy(gameObject);
        bPos=rb.position;
        if (bPos.x < -10)
            Object.Destroy(gameObject);
        if (Input.GetMouseButtonDown(0))
        {
            mPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if((mPos-bPos).magnitude<1)
                move = true;
        }
        if (Input.GetMouseButton(0) && move)
        {
            mPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.position = mPos;
        }
        if (Input.GetMouseButtonUp(0) && move)
            {
                mPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if ((mPos-new Vector2(0.2f,0.7f)).magnitude<1.3f)
                {
                    Gamepaly.score += addscore;
                    rb.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    rb.position = new Vector2(Mathf.Clamp(mPos.x,0-0.53f,0.72f),0.5f);
                    notMove=true;
                }
                else
                    gameObject.transform.position=new Vector2(mPos.x,-3);
                    move=false;
            }
        if(notMove==false)
            rb.velocity = new Vector3(speed, 0f, 0);
        else
             rb.velocity = new Vector3(0, 0f, 0);
    }
}