using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	void Update () {
		if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x,-2.2f,2.2f),Mathf.Clamp(mousePos.y,-4.5f,-0.5f));
            rb.MovePosition(clampedMousePos);
        }
    }
}