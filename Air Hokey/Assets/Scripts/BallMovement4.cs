using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;

public class BallMovement4 : MonoBehaviour, IPunObservable
{
   //private int GamePoint = 5;//point to win the Game
    public static bool InGoal = false;
    public 
    bool send = false,localsend=false;
    private float speed = 15, xVelocity, yVelocity; //just random
    public static Vector3 receivePos, receivedVel;
    Rigidbody2D rb;
    private int ScoreAI, ScoreP;
    public GameObject AIScore, PScore;//UI for Score Display
    private GameController1 gc;
    private PhotonView PV;
    private int xpos, ypos, rxpos, rypos, xvel, yvel, rxvel, ryvel;
    public BoxCollider2D Divider;//Divider between Player and AI
    public Color shady;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            gameObject.GetComponent<SpriteRenderer>().color = shady;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        // AIScore=GameObject.Find("AIScore");
        // PScore=GameObject.Find("PScore");
        // Divider=GameObject.Find("Divider").GetComponent<BoxCollider2D>();
        PV = GetComponent<PhotonView>();
        if(!PhotonNetwork.IsMasterClient)
        PV.RequestOwnership();
        //Ball not to Collide with Divider between Player and AI
        Physics2D.IgnoreCollision(Divider, gameObject.GetComponent<Collider2D>(), true);
        gc = GameObject.Find("GameController").GetComponent<GameController1>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.collider.name);
        GameObject.Find("GameController").GetComponent<audioController>().BallHitSound();
        if (other.collider.name == "Player-2(Clone)")
        {
            localsend=true;
            PV.RPC("SendToggle", RpcTarget.All);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)//Sending and Recieving Position
    {
        if (send)
        {
            if (stream.IsWriting)
            {
                Debug.Log("streaming");
                xpos = ((int)(transform.position.x * 100));  //float to int conversion
                ypos = ((int)(transform.position.y * 100));
                xvel = ((int)(rb.velocity.x * 100));
                yvel = ((int)(rb.velocity.y * 100));
                stream.SendNext(send);
                stream.SendNext(xpos);
                stream.SendNext(ypos);
                stream.SendNext(xvel);
                stream.SendNext(yvel);
                localsend=false;
            }
            else
            {
                send = (bool)stream.ReceiveNext();
                rxpos = (int)stream.ReceiveNext();
                rypos = (int)stream.ReceiveNext();
                rxvel = (int)stream.ReceiveNext();
                ryvel = (int)stream.ReceiveNext();
                receivePos = new Vector2(((float)rxpos) / 100, ((float)rypos) / 100);
                receivedVel = new Vector2(((float)rxvel) / 100, ((float)ryvel) / 100);
                Debug.Log("before Update");
                GameObject.Find("Ball").GetComponent<BallMovement3>().P2trigger();
                LocalUpdate();
                Debug.Log("after Update");
                PV.RPC("SendToggleOff", RpcTarget.All);
            }
        }
    }
    void FixedUpdate()
    {
        if (PV.IsMine)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed); //Speed Limit
    }
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
            return;
        if(localsend)
            return;
        var lag = rb.transform.position - BallMovement3.receivePos;
        // if(lag.magnitude>4 && lag.magnitude<10)
        //     return;
        if (lag.magnitude > 10)
            transform.position = BallMovement3.receivePos;  //teleport
        else if (lag.magnitude > 1.5f)
        {
            //rb.AddRelativeForce(lag.normalized * speed, ForceMode2D.Force);
            // else if(lag.magnitude>1)
            transform.position = Vector2.MoveTowards(transform.position, BallMovement3.receivePos, 0.2f);
            // Debug.Log(rb.velocity);
            // Debug.Log(rb.transform.position);
            // Debug.Log(BallMovement3.receivePos);
        }
        if ((rb.velocity - BallMovement3.receivedVel).magnitude > 0.1)
            rb.velocity = BallMovement3.receivedVel;
        // if (ScoreAI == GamePoint || ScoreP == GamePoint) //Game ENDED
        // {
        //     if (ScoreAI == GamePoint) //Player 2 Won
        //     {
        //         if (PhotonNetwork.IsMasterClient)
        //             gc.Finish("You Won");
        //         else
        //             gc.Finish("You Lose");
        //         GameObject.Find("GameController").GetComponent<audioController>().LoseSound();
        //         ScoreAI = 0;
        //     }
        //     else // Player Won
        //     {
        //         if (PhotonNetwork.IsMasterClient)
        //             gc.Finish("You Lose");
        //         else
        //             gc.Finish("You Won");
        //         GameObject.Find("GameController").GetComponent<audioController>().WinSound();
        //         ScoreP = 0;
        //     }
        //}
    }
    private void RPC_AIGoal()
    {
        GameObject.Find("GameController").GetComponent<audioController>().GoalSound();
        ScoreAI += 1;
        AIScore.GetComponent<TMPro.TextMeshProUGUI>().text = ScoreAI.ToString();
        StartCoroutine("playerReset");
        //yield return null;
        //PlayerMovement2.FirstHitP2 = true;
    }
    private void RPC_PGoal()
    {
        GameObject.Find("GameController").GetComponent<audioController>().GoalSound();
        ScoreP += 1;
        PScore.GetComponent<TMPro.TextMeshProUGUI>().text = ScoreP.ToString();
        StartCoroutine("playerReset");
        //yield return null;
        //PlayerMovement2.FirstHitP1 = true;
    }

    private IEnumerator AIGoal()//Reset Ball Position with a bit Delay
    {
        rb.position = new Vector2(-12f, -12f);//out of screen
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(0.7f);
        rb.transform.Rotate(new Vector3(0f, 0f, 0f));
        rb.position = new Vector2(0f, 4);
        //AI.FirstHit = false;
    }
    private IEnumerator PGoal()//Reset Ball Position with a bit Delay
    {
        rb.position = new Vector2(12f, 12f);//out of screen
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(0.7f);
        rb.transform.Rotate(new Vector3(0f, 0f, 0f));
        rb.position = new Vector2(0f, -4);
        //AI.FirstHit = false;
    }
    public void ResetGame()
    {
        ScoreP = 0;
        PScore.GetComponent<TMPro.TextMeshProUGUI>().text = ScoreP.ToString();
        ScoreAI = 0;
        AIScore.GetComponent<TMPro.TextMeshProUGUI>().text = ScoreAI.ToString();
        rb.position = new Vector2(0f, 0f);
    }
    public IEnumerator playerReset()
    {
        InGoal = true;
        yield return new WaitForSeconds(0.7f);
        InGoal = false;
    }
    [PunRPC]
    public void SendToggle()
    {
        send = true;
        Debug.Log(send);
    }
    [PunRPC]
    public void SendToggleOff()
    {
        send = false;
        print(send);
    }
    public void LocalUpdate()
    {
        rb.transform.position = receivePos;
        rb.velocity = receivedVel;
        Debug.Log("LocalUpdate");
    }
}