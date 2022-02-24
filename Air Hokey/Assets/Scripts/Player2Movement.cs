using UnityEngine;

public class Player2Movement : MonoBehaviour {

    Rigidbody2D rb;
    private Touch TheTouch;
    private Vector2 bounds;
    public float speed=6000;
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
    #if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    void Update(){
        //If Windows this arrows for p1 and aswd for p2 work
        if(Input.GetAxis("Horizontal")!=0 && touchPlayer==1)
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime,0,0));
        if(Input.GetAxis("Vertical")!=0 && touchPlayer==1)
        rb.AddForce(new Vector3(0,Input.GetAxis("Vertical")*speed*Time.deltaTime,0));
        if(Input.GetAxis("Horizontal2")!=0 && touchPlayer==2)
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal2")*speed*Time.deltaTime,0,0));
        if(Input.GetAxis("Vertical2")!=0 && touchPlayer==2)
        rb.AddForce(new Vector3(0,Input.GetAxis("Vertical2")*speed*Time.deltaTime,0));
        if(Input.GetMouseButtonDown(0))
        Debug.Log("mouse clicked");
    }
    #endif
    #if UNITY_STANDALONE_ANDROID
    void Update ()
    {
        //If Android this will work
        Debug.Log(Application.platform);
		if (Input.touchCount>0){
            if((Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y<0 && touchPlayer==1) || (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y>0 && touchPlayer==2)){
            TheTouch=Input.GetTouch(0);
            MovePlayer();
            }
            else if((Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y<0 && touchPlayer==1) || (Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y>0 && touchPlayer==2)){
            TheTouch=Input.GetTouch(1);
            MovePlayer();
            }
        }
    }
    #endif
    private void MovePlayer(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x,-2.2f,2.2f),Mathf.Clamp(mousePos.y,bounds.x,bounds.y));
        rb.MovePosition(clampedMousePos);
    }
}