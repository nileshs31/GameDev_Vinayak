using UnityEngine;
using UnityEngine.UI;

public class WindowsMovement : MonoBehaviour {

    Rigidbody2D rb;
    private float Player;
    public float speed=6000;
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        if(gameObject.name=="Player2")
        Player=2;
        else
        Player=1;
    }
	void Update ()
    {
        if(Input.GetAxis("Horizontal")!=0 && Player==1)
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime,0,0));
        if(Input.GetAxis("Vertical")!=0 && Player==1)
        rb.AddForce(new Vector3(0,Input.GetAxis("Vertical")*speed*Time.deltaTime,0));
        if(Input.GetAxis("Horizontal2")!=0 && Player==2)
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal2")*speed*Time.deltaTime,0,0));
        if(Input.GetAxis("Vertical2")!=0 && Player==2)
        rb.AddForce(new Vector3(0,Input.GetAxis("Vertical2")*speed*Time.deltaTime,0));
    }
}