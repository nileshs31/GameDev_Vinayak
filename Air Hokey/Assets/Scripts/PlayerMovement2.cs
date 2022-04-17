using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System.IO;

public class PlayerMovement2 : MonoBehaviour,IPunObservable
{

    Rigidbody2D rb;
    private Touch TheTouch;
    private Vector2 bounds,target;
    private Vector3 receivePos;
    private float touchPlayer;
    private PhotonView view;
    private void Awake() {
        PhotonNetwork.SendRate=15;
        PhotonNetwork.SerializationRate=10;
    }
    void Start()
    {
        view=GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(gameObject.name);
        if (gameObject.name == "Player-2(Clone)"){
            touchPlayer = 2;
            bounds=new Vector2(1.5f,17);
        }
        else{
            bounds=new Vector2(-16f,-1.5f);
            touchPlayer = 1;
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting)
            //if(view.IsMine)
            stream.SendNext(transform.position);
        else
            receivePos=(Vector3)stream.ReceiveNext();
    }
    void Update()
    {
        if(!view.IsMine){
            var lag=rb.transform.position-receivePos;
            if(lag.magnitude!=0)
            Debug.Log(lag.magnitude);
            target=Vector2.Lerp(rb.transform.position,receivePos,0.3f);
            return;
        }
        if (Input.GetAxis("Horizontal") != 0 && touchPlayer == 1)
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * 20000 * Time.deltaTime, 0, 0));
        if (Input.GetAxis("Vertical") != 0 && touchPlayer == 1)
            rb.AddForce(new Vector3(0, Input.GetAxis("Vertical") * 20000 * Time.deltaTime, 0));
        if (Input.GetAxis("Horizontal2") != 0 && touchPlayer == 2)
            rb.AddForce(new Vector3(-Input.GetAxis("Horizontal2") * 20000 * Time.deltaTime,0, 0));
        if (Input.GetAxis("Vertical2") != 0 && touchPlayer == 2)
            rb.AddForce(new Vector3(0,-Input.GetAxis("Vertical2") * 20000 * Time.deltaTime, 0));

        if (Input.touchCount > 0)
            {
                TheTouch = Input.GetTouch(0);
                //view.RPC("MovePlayer",RpcTarget.All);
                MovePlayer();
            }
            //if (BallMovement.GaolIN == 1)
                //StartCoroutine("ResetPlayer");
    }
    private void FixedUpdate() {
        if(!view.IsMine)
        rb.MovePosition(target);
    }
    //[PunRPC]
    private void MovePlayer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        //Debug.Log(mousePos);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, -5.5f, 5f), Mathf.Clamp(mousePos.y,bounds.x,bounds.y));
        //Debug.Log(clampedMousePos);
        rb.MovePosition(clampedMousePos);
    }

    private IEnumerator ResetPlayer()
    {
        rb.position = new Vector2(10f, 15f);
        yield return new WaitForSeconds(0.5f);
        if (touchPlayer == 1)
            rb.position = new Vector2(0f, -3.75f);
        if (touchPlayer == 2)
            rb.position = new Vector2(0f, 3.75f);
    }
}