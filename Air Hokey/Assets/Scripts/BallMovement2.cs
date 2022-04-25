using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;

public class BallMovement2 : MonoBehaviour, IPunObservable
{
    private int GamePoint = 5;//point to win the Game
    public static bool InGoal=false;
    private float speed = 15, xVelocity, yVelocity; //just random
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
        if(!PV.IsMine)
        return;
        if (other.collider.name == "GoalAI")
        {   // AI has Scored a Goal
            PV.RPC("RPC_AIGoal",RpcTarget.All);
            //RPC_AIGoal();
            StartCoroutine("AIGoal");
        }
        if (other.collider.name == "GoalP")
        {   // Player has Scored a Goal
            PV.RPC("RPC_PGoal",RpcTarget.All);
            //RPC_PGaol();
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
        }
    }
    void FixedUpdate()
    {
        if(PV.IsMine)
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed); //Speed Limit
        if (!PV.IsMine)
        {
            receivePos = new Vector2(((float)rxpos) / 100, ((float)rypos) / 100);   //int to float conversion
            var lag = rb.transform.position - receivePos;
            if(lag.magnitude>7)
            transform.position=receivePos;  //teleport
            else if (lag.magnitude>1.5f)
            transform.position = Vector2.MoveTowards(transform.position, receivePos,speed*Time.fixedDeltaTime);
            //target=Vector2.Lerp(rb.transform.position,receivePos,0.8f);
            //return;
        }
    }
    void Update()
    {
        // if (transform.position.y > 5 || transform.position.y < -5)
        // {
        //     PlayerMovement2.FirstHitP1 = false;
        //     PlayerMovement2.FirstHitP2 = false;
        // }
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
        // if(PV.IsMine){
        //     return;
        // }
        // else
        //     StartCoroutine("Ownerships");
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
    public IEnumerator Ownerships(){
        if(PhotonNetwork.IsMasterClient && transform.position.y<0){
            PV.RequestOwnership();
            yield return new WaitForSeconds(0.2f);
        }
        else if(!PhotonNetwork.IsMasterClient && transform.position.y>0){
            PV.RequestOwnership();
            yield return new WaitForSeconds(0.2f);
        }
    }
    public IEnumerator playerReset(){
        InGoal=true;
        yield return new WaitForSeconds(0.7f);
        InGoal=false;
    }
}