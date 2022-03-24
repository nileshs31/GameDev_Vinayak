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
        bounds = new Vector2(-4.4f,-0.6f);
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
    }
    private void MovePlayer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, -1.5f, 1.4f), Mathf.Clamp(mousePos.y, bounds.x, bounds.y));
        rb.MovePosition(clampedMousePos);
    }
}