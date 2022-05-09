using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;

public class BallMovement3 : MonoBehaviour, IPunObservable
{
    private int GamePoint = 5;//point to win the Game
    public static bool InGoal=false;
    private float speed = 15, xVelocity, yVelocity; //just random
    public static Vector3 receivePos;
    public static Vector2 receivedVel;
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
        if(!PV.IsMine){
            gameObject.GetComponent<SpriteRenderer>().enabled=false;
            gameObject.GetComponent<CircleCollider2D>().enabled=false;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(!PV.IsMine)
        return;
        GameObject.Find("GameController").GetComponent<audioController>().BallHitSound();
        if (other.collider.name == "GoalAI")
        {   // AI has Scored a Goal
            PV.RPC("RPC_AIGoal",RpcTarget.All);
            StartCoroutine("AIGoal");
        }
        if (other.collider.name == "GoalP")
        {   // Player has Scored a Goal
            PV.RPC("RPC_PGoal",RpcTarget.All);
            StartCoroutine("PGoal");
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)//Sending and Recieving Position
    {
        if (stream.IsWriting)
        {
            xpos = ((int)(transform.position.x * 100));  //float to int conversion
            ypos = ((int)(transform.position.y * 100));
            stream.SendNext(xpos);
            stream.SendNext(ypos);
        }
        else
        {
            rxpos = (int)stream.ReceiveNext();
            rypos = (int)stream.ReceiveNext();
            receivePos = new Vector2(((float)rxpos) / 100, ((float)rypos) / 100);   //int to float conversion
        if(rb.velocity.x>14)
            BallMovement3.receivePos.x+=0.8f;    
            else if(rb.velocity.x>10)
            BallMovement3.receivePos.x+=0.6f;
            else if(rb.velocity.x>5)
            BallMovement3.receivePos.x+=0.3f;
        if(rb.velocity.x<-14)
            BallMovement3.receivePos.x-=0.8f;    
            else if(rb.velocity.x<-10)
            BallMovement3.receivePos.x-=0.6f;
            else if(rb.velocity.x<-5)
            BallMovement3.receivePos.x-=0.3f;
        if(rb.velocity.y>14)
            BallMovement3.receivePos.y+=0.8f;    
            else if(rb.velocity.y>10)
            BallMovement3.receivePos.y+=0.6f;
            else if(rb.velocity.x>5)
            BallMovement3.receivePos.y+=0.3f;
        if(rb.velocity.y<-14)
            BallMovement3.receivePos.y-=0.8f;    
            else if(rb.velocity.y<-10)
            BallMovement3.receivePos.y-=0.6f;
            else if(rb.velocity.x<-5)
            BallMovement3.receivePos.y-=0.3f;
        }
    }
    void FixedUpdate()
    {
        if(PV.IsMine)
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed); //Speed Limit
        if (!PV.IsMine)
        {
            receivedVel=rb.velocity;
            transform.position=receivePos;
        }
    }
    void Update()
    {
        if(!PV.IsMine)
        return;
        if (ScoreAI == GamePoint || ScoreP == GamePoint) //Game ENDED
        {
            if (ScoreAI == GamePoint) //Player 2 Won
            {
                if (PhotonNetwork.IsMasterClient)
                    gc.Finish("You Won");
                else
                    gc.Finish("You Lose");
                GameObject.Find("GameController").GetComponent<audioController>().LoseSound();
                ScoreAI = 0;
            }
            else // Player Won
            {
                if (PhotonNetwork.IsMasterClient)
                    gc.Finish("You Lose");
                else
                    gc.Finish("You Won");
                GameObject.Find("GameController").GetComponent<audioController>().WinSound();
                ScoreP = 0;
            }
        }
    }
    [PunRPC]
    private void RPC_AIGoal()
    {
        GameObject.Find("GameController").GetComponent<audioController>().GoalSound();
        ScoreAI += 1;
        AIScore.GetComponent<TMPro.TextMeshProUGUI>().text = ScoreAI.ToString();
        StartCoroutine("playerReset");
        //yield return null;
        //PlayerMovement2.FirstHitP2 = true;
    }
    [PunRPC]
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
    public IEnumerator playerReset(){
        InGoal=true;
        yield return new WaitForSeconds(0.7f);
        InGoal=false;
    }
}