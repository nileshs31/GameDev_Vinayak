using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
    private Touch TheTouch;
    private Vector2 bounds;
    private float touchPlayer;
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
		if (Input.touchCount>0){
            if((Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y<0 && touchPlayer==1) || (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y>0 && touchPlayer==2)){
            TheTouch=Input.GetTouch(0);
            MovePlayer();
            }
        }
        else if(Input.touchCount>1){
            if((Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y<0 && touchPlayer==1) || (Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y>0 && touchPlayer==2)){
            TheTouch=Input.GetTouch(1);
            MovePlayer();
            }
        }
    }
    private void MovePlayer(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x,-2.2f,2.2f),Mathf.Clamp(mousePos.y,bounds.x,bounds.y));
        rb.MovePosition(clampedMousePos);
    }
}
