using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1 : MonoBehaviour
{
    Rigidbody2D rb;
    private Touch TheTouch;
    private Vector2 bounds; //Not to GO Outside of Screen or Table or Limit
    public static bool p1t0 = false; //Lock the Touch(0) for Player 1
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bounds = new Vector2(-4.5f, -0.5f);
    }
    void Update()
    {
        if (Input.touchCount > 0)
        { // 1st Touch Happens
            if (!P2.p2t0)
            { //Touch(0) is not Locked
                if (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y < 0)
                {
                    p1t0 = true;
                    TheTouch = Input.GetTouch(0);
                    MovePlayer();
                    if (TheTouch.phase == TouchPhase.Ended)
                    {
                        p1t0 = false;
                    }
                }
            }
            else if (Input.touchCount > 1)
            { //2nd Touch Happens
                if (Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y < 0)
                {
                    TheTouch = Input.GetTouch(1);
                    MovePlayer();
                }
            }
        }
        if(BallMovement.GaolIN==1)
        StartCoroutine("ResetPlayer");

    }
    private void MovePlayer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, -2.2f, 2.2f), Mathf.Clamp(mousePos.y, bounds.x, bounds.y));
        if(BallMovement.GaolIN==0)
        rb.MovePosition(clampedMousePos);
    }

    private IEnumerator ResetPlayer(){
        rb.position=new Vector2(10f,15f);
        yield return new WaitForSeconds(0.5f);
        rb.position=new Vector2(0f,-3.75f);
    }
}