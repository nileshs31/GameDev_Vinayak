using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3 : MonoBehaviour
{
    Rigidbody2D rb;
    private Touch TheTouch;
    int Player;
    private Vector2 bounds; //Not to GO Outside of Screen or Table or Limit
    public static bool p1t0 = false; //Lock the Touch(0) for Player 1
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bounds = new Vector2(-4.4f,-0.6f);
    }
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Debug.Log(Input.touchCount);
            Debug.Log(Input.GetTouch(0).fingerId);
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
            // Debug.Log(Input.GetTouch(1).fingerId);
            // Debug.Log(Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position));
            //Debug.Log(Input.GetTouch(2).fingerId);
        }
    }

    void MovePlayer()
    {
        //Debug.Log("moving");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, -1.5f, 1.4f), Mathf.Clamp(mousePos.y, bounds.x, bounds.y));
        rb.MovePosition(clampedMousePos);
    }
}