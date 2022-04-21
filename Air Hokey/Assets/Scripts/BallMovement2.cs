using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;

public class BallMovement2 : MonoBehaviour//, IPunObservable
{
    private int GamePoint = 5;//point to win the Game
    private float speed = 15, xVelocity, yVelocity;
    private Vector3 receivePos;
    private Vector2 target;
    Rigidbody2D rb;
    private int ScoreAI, ScoreP;
    public GameObject AIScore, PScore;//UI for Score Display
    private GameController1 gc;
    private PhotonView PV;
    public Collider2D Divider;//Divider between Player and AI
    private int xpos, ypos, rxpos, rypos; //Type Casting to reduce Lag
    void Start()
    {
        PV = GetComponent<PhotonView>();
        //Ball not to Collide with Divider between Player and AI
        Physics2D.IgnoreCollision(Divider, gameObject.GetComponent<Collider2D>(), true);
        gc = GameObject.Find("GameController").GetComponent<GameController1>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject.Find("GameController").GetComponent<audioController>().BallHitSound();
        if (other.collider.name == "GoalAI")
        {   // AI has Scored a Goal
            //PV.RPC("RPC_AIGoal",RpcTarget.All);
            RPC_AIGoal();
        }
        else if (other.collider.name == "GoalP")
        {   // Player has Scored a Goal
            //PV.RPC("RPC_PGoal",RpcTarget.All);
            RPC_PGaol();
        }
    }
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         xpos = ((int)(transform.position.x * 10));
    //         ypos = ((int)(transform.position.y * 10));
    //         stream.SendNext(xpos);
    //         stream.SendNext(ypos);
    //     }
    //     else
    //     {
    //         rxpos = (int)stream.ReceiveNext();
    //         rypos = (int)stream.ReceiveNext();
    //     }
    // }
    void FixedUpdate()
    {
        if(PhotonNetwork.IsMasterClient)
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed); //Speed Limit
        // if (!PV.IsMine)
        // {
        //     receivePos = new Vector2(((float)rxpos) / 10, ((float)rypos) / 10);
        //     // var lag = rb.transform.position - receivePos;
        //     // if (lag.magnitude != 0)
        //     //     Debug.Log(lag.magnitude);
        //     transform.position =Vector2.MoveTowards(transform.position, receivePos,speed*Time.deltaTime);
        //     //target=Vector2.Lerp(rb.transform.position,receivePos,0.8f);
        //     //return;
        // }
    }
    void Update()
    {
        if (transform.position.y > 5 || transform.position.y < -5)
        {
            PlayerMovement2.FirstHitP1 = false;
            PlayerMovement2.FirstHitP2 = false;
        }
        if (ScoreAI == GamePoint || ScoreP == GamePoint) //Game ENDED
        {
            if (ScoreAI == GamePoint) //Player 2 Won
            {
                if (PhotonNetwork.IsMasterClient)
                    gc.Finish("You Lose");
                else
                    gc.Finish("You Won");
                GameObject.Find("GameController").GetComponent<audioController>().LoseSound();
                ScoreAI = 0;
            }
            else // Player Won
            {
                if (PhotonNetwork.IsMasterClient)
                    gc.Finish("You Won");
                else
                    gc.Finish("You Lose");
                GameObject.Find("GameController").GetComponent<audioController>().WinSound();
                ScoreP = 0;
            }
        }
    }
    //[PunRPC]
    private void RPC_AIGoal()
    {
        GameObject.Find("GameController").GetComponent<audioController>().GoalSound();
        StartCoroutine("AIGoal");
        rb.position = new Vector2(-12f, -12f);//out of screen
        rb.velocity = new Vector2(0f, 0f);
        ScoreAI += 1;
        AIScore.GetComponent<TMPro.TextMeshProUGUI>().text = ScoreAI.ToString();
        PlayerMovement2.FirstHitP2 = true;
    }
    //[PunRPC]
    private void RPC_PGaol()
    {
        GameObject.Find("GameController").GetComponent<audioController>().GoalSound();
        StartCoroutine("PGoal");
        rb.position = new Vector2(-10f, -10f);
        rb.velocity = new Vector2(0f, 0f);
        ScoreP += 1;
        PScore.GetComponent<TMPro.TextMeshProUGUI>().text = ScoreP.ToString();
        PlayerMovement2.FirstHitP1 = true;
    }

    private IEnumerator AIGoal()//Reset Ball Position with a bit Delay
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.2f);
        rb.transform.Rotate(new Vector3(0f, 0f, 0f));
        rb.position = new Vector2(0f, -1);
        //AI.FirstHit = false;
    }
    private IEnumerator PGoal()//Reset Ball Position with a bit Delay
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.2f);
        rb.transform.Rotate(new Vector3(0f, 0f, 0f));
        rb.position = new Vector2(0f, 1);
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

}