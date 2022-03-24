using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System.IO;

public class PlayerMovement2 : MonoBehaviour
{

    Rigidbody2D rb;
    private Touch TheTouch;
    private Vector2 bounds;
    private float touchPlayer;
    private PhotonView view;
    void Start()
    {
        Debug.Log(gameObject.name + PhotonNetwork.IsMasterClient);
        view=GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(gameObject.name);
        if (gameObject.name == "Player-2")
        {
            bounds = new Vector2(0.5f, 4.5f);
            touchPlayer = 2;
            //Camera.main.transform.Rotate(new Vector3(0,0,180));
        }
        else
        {
            bounds = new Vector2(-4.5f, -0.5f);
            touchPlayer = 1;
        }
    }
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && touchPlayer == 1 && PhotonNetwork.IsMasterClient)
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * 600 * Time.deltaTime, 0, 0));
        if (Input.GetAxis("Vertical") != 0 && touchPlayer == 1 && PhotonNetwork.IsMasterClient)
            rb.AddForce(new Vector3(0, Input.GetAxis("Vertical") * 600 * Time.deltaTime, 0));
        if (Input.GetAxis("Horizontal2") != 0 && touchPlayer == 2 && (!PhotonNetwork.IsMasterClient))
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal2") * 600 * Time.deltaTime, 0, 0));
        if (Input.GetAxis("Vertical2") != 0 && touchPlayer == 2 && (!PhotonNetwork.IsMasterClient))
            rb.AddForce(new Vector3(0, Input.GetAxis("Vertical2") * 600 * Time.deltaTime, 0));
        if ((PhotonNetwork.IsMasterClient && touchPlayer == 1) || (touchPlayer == 2 && (!PhotonNetwork.IsMasterClient)))
        {
            if (Input.touchCount > 0)
            {
                TheTouch = Input.GetTouch(0);
                view.RPC("MovePlayer",RpcTarget.All);
            }
            //if (BallMovement.GaolIN == 1)
                //StartCoroutine("ResetPlayer");
        }
    }
    [PunRPC]
    private void MovePlayer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, -1.5f, 1.4f), Mathf.Clamp(mousePos.y, bounds.x, bounds.y));
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