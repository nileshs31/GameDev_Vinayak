using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System.IO;

public class PlayerMovement2 : MonoBehaviour//, IPunObservable
{

    Rigidbody2D rb;
    public Sprite Red,Blue;
    [Range(0f, 1f)] public float positionStrength = 1f;
    // [Range(0f,1f)] public float rotationStrength = 1f;
    private Touch TheTouch;
    private Vector2 bounds, target;
    private Vector3 receivePos,Velocity;
    private float touchPlayer;
    private float xVelocity, yVelocity, velocity = 90000;   //random Variable
    private PhotonView view;
    private bool isMoving = true;
    //public static bool FirstHitP1 = true, FirstHitP2 = true;   // adding Velocity to player to be able to move the ball for first time
    private int xpos, ypos, rxpos, rypos; //Type Casting to reduce Lag
    // private void Awake()
    // {
    //     PhotonNetwork.SendRate = 8;
    //     PhotonNetwork.SerializationRate = 6;
    // }
    void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        //Debug.Log(gameObject.name);
        if (gameObject.name == "Player-2(Clone)")
        {
            touchPlayer = 2;
            bounds = new Vector2(1.5f, 17);
        }
        else
        {
            bounds = new Vector2(-16f, -1.5f);
            touchPlayer = 1;
        }
        if(!PhotonNetwork.IsMasterClient){
            if(touchPlayer==1){
                gameObject.GetComponent<SpriteRenderer>().sprite=Red;
            }
            if(touchPlayer==2){
                gameObject.GetComponent<SpriteRenderer>().sprite=Blue;
            }
        }
    }
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         //if(view.IsMine)
    //         if (isMoving)
    //         {
    //             Debug.Log(rb.velocity);
    //             xpos = ((int)(transform.position.x * 100)); //Flaot to int conversion
    //             ypos = ((int)(transform.position.y * 100));
    //             stream.SendNext(xpos);
    //             stream.SendNext(ypos);
    //         }
    //     }
    //     else
    //     {
    //         rxpos = (int)stream.ReceiveNext();
    //         rypos = (int)stream.ReceiveNext();
    //         receivePos = new Vector2(((float)rxpos) / 100, ((float)rypos) / 100);
    //         //var lag = new Vector2(rb.transform.position.x - receivePos.x, rb.transform.position.y - receivePos.y);
    //         //Debug.Log(lag);
    //     }
    // }
    void Update()
    {
        if (!view.IsMine)
        {
            return;
        }
        StartCoroutine("Moving");
        if (Input.GetAxis("Horizontal") != 0 && touchPlayer == 1)
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * velocity*10 * Time.deltaTime, 0, 0));
        if (Input.GetAxis("Vertical") != 0 && touchPlayer == 1)
            rb.AddForce(new Vector3(0, Input.GetAxis("Vertical") * velocity*10 * Time.deltaTime, 0));
        if (Input.GetAxis("Horizontal2") != 0 && touchPlayer == 2)
            rb.AddForce(new Vector3(-Input.GetAxis("Horizontal2") * velocity*10 * Time.deltaTime, 0, 0));
        if (Input.GetAxis("Vertical2") != 0 && touchPlayer == 2)
            rb.AddForce(new Vector3(0, -Input.GetAxis("Vertical2") * velocity*10 * Time.deltaTime, 0));

        if (Input.touchCount > 0)
        {
            TheTouch = Input.GetTouch(0);
            //view.RPC("MovePlayer",RpcTarget.All);
            MovePlayer();
        }
        if(BallMovement3.InGoal==true && !isMoving){
            StartCoroutine("ResetPlayer");
            //Debug.Log(isMoving);
        }
        //if (BallMovement.GaolIN == 1)
        //StartCoroutine("ResetPlayer");
    }
    // private void FixedUpdate() {
    //     if(!view.IsMine)
    //     rb.MovePosition(target);
    // }
    private void FixedUpdate()
    {
        //rb.velocity = Vector2.ClampMagnitude(rb.velocity, velocity);
        if (!view.IsMine)
        {
            // if ((FirstHitP1 && touchPlayer == 1) || (FirstHitP2 && touchPlayer == 2))
            // {
            //     target = Vector2.Lerp(rb.transform.position, receivePos,0.3f);
            //     rb.MovePosition(target);
            // }
            // else
            // transform.position = Vector2.MoveTowards(transform.position, receivePos, velocity * Time.deltaTime);
            //Vector2 deltaPos = receivePos - transform.position;
            //Debug.Log(deltaPos.magnitude);
            //if (deltaPos.magnitude > 0.6f)
                //rb.AddRelativeForce(deltaPos.normalized * velocity, ForceMode2D.Force);
            // else if(deltaPos.magnitude>0.1f){
            //     rb.velocity=Vector3.zero;
            //     transform.position = Vector2.MoveTowards(transform.position, receivePos, velocity * Time.fixedDeltaTime);
            // }
            // else if(deltaPos.magnitude<0.01f)
            // return;
            // else
            // {
            //     rb.velocity=Vector3.zero;
            //     rb.MovePosition(receivePos);
            // }
        }
    }
    //[PunRPC]
    private void MovePlayer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        //Debug.Log(mousePos);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, -5.5f, 5f), Mathf.Clamp(mousePos.y, bounds.x, bounds.y));
        //Debug.Log(clampedMousePos);
        //Vector2 Finalpos=new Vector2(Mathf.MoveTowards(transform.position.x,clampedMousePos.x,velocity*Time.deltaTime),Mathf.MoveTowards(transform.position.y,clampedMousePos.y,velocity*Time.deltaTime));
        rb.MovePosition(clampedMousePos);
        // transform.position=Vector2.MoveTowards(transform.position,clampedMousePos,velocity*Time.deltaTime)
        // rb.velocity = 1f/Time.fixedDeltaTime * deltaPos * Mathf.Pow(positionStrength, velocity*Time.fixedDeltaTime);

    }

    public IEnumerator ResetPlayer()
    {
        if (touchPlayer == 1)
            rb.position = new Vector2(0f, -14);
        if (touchPlayer == 2)
            rb.position = new Vector2(0f, 14);
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator Moving()
    {
        isMoving = true;
        var tmpos = transform.position;
        yield return new WaitForSeconds(0.09f);
        Velocity=(transform.position-tmpos)/Time.deltaTime;
        var nwpos = transform.position;
        if (tmpos == nwpos)
            isMoving = false;
    }
}