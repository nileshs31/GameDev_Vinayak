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
            Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x,-2.2f,2.2f),Mathf.Clamp(mousePos.y,-4.5f,-0.5f));
            //if(mousePos.y<-0.5 && mousePos.y>-4.5)
            //rb.position=new Vector2 (rb.position.x,mousePos.y);
            //if(mousePos.x<2.2 && mousePos.x>-2.2)
            //rb.position=new Vector2 (mousePos.x,rb.position.y);
            rb.MovePosition(clampedMousePos);
        }
    }
}