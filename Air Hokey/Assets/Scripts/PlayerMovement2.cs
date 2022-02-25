using UnityEngine;

public class PlayerMovement2 : MonoBehaviour {

    Rigidbody2D rb;
    private Touch TheTouch;
    private Vector2 bounds;
    private float touchPlayer;
    public static bool p1t0=false,p1t1=false,p2t0=false,p2t1=false;
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        if(gameObject.name=="Player2"){
        bounds=new Vector2(0.5f,4.5f);
        touchPlayer=2;
        }
        else{
        bounds=new Vector2(-4.5f,-0.5f);
        touchPlayer=1;
        }
    }
	void Update ()
    {
		if (touchPlayer==1){
            if(Input.touchCount>0){
                if(!p2t0){
                    if(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y<0){
                        p1t0=true;
                        TheTouch=Input.GetTouch(0);
                        MovePlayer();
                        if(TheTouch.phase==TouchPhase.Ended){
                            p1t0=false;
                        }
                    }
                }
                else if(!p2t1){
                    if(Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y<0){
                        p1t1=true;
                        TheTouch=Input.GetTouch(1);
                        MovePlayer();
                        if(TheTouch.phase==TouchPhase.Ended){
                            p1t1=false;
                        }
                    }
                }
            }
        }
		else{
            if(Input.touchCount>0){
                if(!p1t0){
                    if(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y>0){
                        p2t0=true;
                        TheTouch=Input.GetTouch(0);
                        MovePlayer();
                        if(TheTouch.phase==TouchPhase.Ended){
                            p2t0=false;
                        }
                    }
                }
                else if(!p1t1){
                    if(Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y<0){
                        p2t1=true;
                        TheTouch=Input.GetTouch(1);
                        MovePlayer();
                        if(TheTouch.phase==TouchPhase.Ended){
                            p2t1=false;
                        }
                    }
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