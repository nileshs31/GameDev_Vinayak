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
        view=GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(gameObject.name);
        if (gameObject.name == "Player-2(Clone)")
        {
            bounds = new Vector2(15.5f, 2f);
            touchPlayer = 2;
            //Camera.main.transform.Rotate(new Vector3(0,0,180));
        }
        else
        {
            bounds = new Vector2(-15.5f, -2f);
            touchPlayer = 1;
        }
    }
    void Update()
    {
        if(!view.IsMine)
        return;
        if (Input.GetAxis("Horizontal") != 0 && touchPlayer == 1)
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * 1000 * Time.deltaTime, 0, 0));
        if (Input.GetAxis("Vertical") != 0 && touchPlayer == 1)
            rb.AddForce(new Vector3(0, Input.GetAxis("Vertical") * 1000 * Time.deltaTime, 0));
        if (Input.GetAxis("Horizontal2") != 0 && touchPlayer == 2)
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal2") * 1000 * Time.deltaTime, 0, 0));
        if (Input.GetAxis("Vertical2") != 0 && touchPlayer == 2)
            rb.AddForce(new Vector3(0, Input.GetAxis("Vertical2") * 1000 * Time.deltaTime, 0));

        if (Input.touchCount > 0)
            {
                TheTouch = Input.GetTouch(0);
                //view.RPC("MovePlayer",RpcTarget.All);
                MovePlayer();
            }
            //if (BallMovement.GaolIN == 1)
                //StartCoroutine("ResetPlayer");
    }
    //[PunRPC]
    private void MovePlayer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(TheTouch.position);
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, -5.5f, 5f), Mathf.Clamp(mousePos.y, bounds.x, bounds.y));
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