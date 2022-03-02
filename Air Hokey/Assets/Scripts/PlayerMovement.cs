using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    private Touch TheTouch;
    private Vector2 bounds; //Not to GO Outside of Screen or Table or Limit
    public float speed = 6000;
    private float touchPlayer; //Player on witch Script is Attached
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.name == "Player2")
        {
            if (Application.platform == RuntimePlatform.Android)
                gameObject.GetComponent<P2>().enabled = true;   //Android Movement Script
            else
                touchPlayer = 2;
        }
        else // Attached on Player 1
        {
            if (Application.platform == RuntimePlatform.Android)    //Android Movement Script
                gameObject.GetComponent<P1>().enabled = true;
            else
                touchPlayer = 1;
        }
    }
    void Update()
    {
        if (Application.platform != RuntimePlatform.Android && BallMovement.GaolIN==0)
        {
            //If on Windows, this arrows for p1 and aswd for p2 work
            if (Input.GetAxis("Horizontal") != 0 && touchPlayer == 1)
                rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0));
            if (Input.GetAxis("Vertical") != 0 && touchPlayer == 1)
                rb.AddForce(new Vector3(0, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0));
            if (Input.GetAxis("Horizontal2") != 0 && touchPlayer == 2)
                rb.AddForce(new Vector3(Input.GetAxis("Horizontal2") * speed * Time.deltaTime, 0, 0));
            if (Input.GetAxis("Vertical2") != 0 && touchPlayer == 2)
                rb.AddForce(new Vector3(0, Input.GetAxis("Vertical2") * speed * Time.deltaTime, 0));
        }
        if(BallMovement.GaolIN==1 && touchPlayer==1)
        StartCoroutine("ResetPlayer1");
        if(BallMovement.GaolIN==1 && touchPlayer==2)
        StartCoroutine("ResetPlayer2");
    }
    private IEnumerator ResetPlayer2(){
        rb.position = new Vector2(10f,10f);
        yield return new WaitForSeconds(0.5f);
        rb.velocity=new Vector2(0f,0f);
        rb.position=new Vector2(0f,3.75f);
    }
    private IEnumerator ResetPlayer1(){
        rb.position = new Vector2(15f,10f);
        yield return new WaitForSeconds(0.5f);
        rb.velocity=new Vector2(0f,0f);
        rb.position=new Vector2(0f,-3.75f);
    }

}