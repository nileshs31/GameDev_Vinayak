using UnityEngine;

public class P2 : MonoBehaviour
{
    Rigidbody2D rb;
    private Touch TheTouch;
    private Vector2 bounds;
    public static bool p2t0=false;
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        bounds=new Vector2(0.5f,4.5f);
    }
	void Update ()
    {
            if(Input.touchCount>0){
                if(!P1.p1t0){
                    if(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y>0){
                        p2t0=true;
                        TheTouch=Input.GetTouch(0);
                        MovePlayer();
                        if(TheTouch.phase==TouchPhase.Ended){
                            p2t0=false;
                        }
                    }
                }
                else if(Input.touchCount>1){
                    if(Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y>0){
                        TheTouch=Input.GetTouch(1);
                        MovePlayer();
                    }
                }
            }
        }
    private void MovePlayer(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x,-2.2f,2.2f),Mathf.Clamp(mousePos.y,bounds.x,bounds.y));
        rb.MovePosition(clampedMousePos);
    }
}