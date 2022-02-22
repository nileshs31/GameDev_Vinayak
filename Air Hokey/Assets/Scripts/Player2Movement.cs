using UnityEngine;
using UnityEngine.UI;

public class Player2Movement : MonoBehaviour {

    Rigidbody2D rb;
    private Touch TheTouch;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	void Update () {
		if (Input.touchCount>0){
        if(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y>0){
        TheTouch=Input.GetTouch(0);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
            Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x,-2.2f,2.2f),Mathf.Clamp(mousePos.y,0.5f,4.5f));
            rb.MovePosition(clampedMousePos);
        }
        else if(Input.touchCount>1)
        if(Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y>0){
        TheTouch=Input.GetTouch(1);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
            Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x,-2.2f,2.2f),Mathf.Clamp(mousePos.y,0.5f,4.5f));
            rb.MovePosition(clampedMousePos);
        }
    }
    }
}