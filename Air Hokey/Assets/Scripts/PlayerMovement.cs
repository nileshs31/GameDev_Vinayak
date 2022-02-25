using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    private Touch TheTouch;
    private Vector2 bounds;
    public float speed = 6000;
    private float touchPlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.name == "Player2")
        {
            if (Application.platform == RuntimePlatform.Android)
                gameObject.GetComponent<P2>().enabled = true;
            else
                touchPlayer = 2;
        }
        else
        {
            if (Application.platform == RuntimePlatform.Android)
                gameObject.GetComponent<P1>().enabled = true;
            else
                touchPlayer = 1;
        }
    }
    void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            //If Windows this arrows for p1 and aswd for p2 work
            if (Input.GetAxis("Horizontal") != 0 && touchPlayer == 1)
                rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0));
            if (Input.GetAxis("Vertical") != 0 && touchPlayer == 1)
                rb.AddForce(new Vector3(0, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0));
            if (Input.GetAxis("Horizontal2") != 0 && touchPlayer == 2)
                rb.AddForce(new Vector3(Input.GetAxis("Horizontal2") * speed * Time.deltaTime, 0, 0));
            if (Input.GetAxis("Vertical2") != 0 && touchPlayer == 2)
                rb.AddForce(new Vector3(0, Input.GetAxis("Vertical2") * speed * Time.deltaTime, 0));
        }
    }
}