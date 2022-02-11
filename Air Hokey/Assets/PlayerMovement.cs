using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        //startingPosition = rb.position;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(mousePos.x<2.2 && mousePos.x>-2.2 && mousePos.y<-0.5 && mousePos.y>-4.5){
            //Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x,-0.062f,0.062f),Mathf.Clamp(mousePos.y,-0.044f,0.044f));
            rb.position=mousePos;
            }
        }
    }
}