using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;

public class BallMovement4 : MonoBehaviour
{
    private int GamePoint = 5;//point to win the Game
    public static bool InGoal=false;
    private float speed = 15, xVelocity, yVelocity; //just random
    private Vector3 receivePos;
    Rigidbody2D rb;
    private int ScoreAI, ScoreP;
    public GameObject AIScore, PScore;//UI for Score Display
    private GameController1 gc;
    private PhotonView PV;
    public BoxCollider2D Divider;//Divider between Player and AI
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        gameObject.SetActive(false);
        // AIScore=GameObject.Find("AIScore");
        // PScore=GameObject.Find("PScore");
        // Divider=GameObject.Find("Divider").GetComponent<BoxCollider2D>();
        PV = GetComponent<PhotonView>();
        //Ball not to Collide with Divider between Player and AI
        Physics2D.IgnoreCollision(Divider, gameObject.GetComponent<Collider2D>(), true);
        gc = GameObject.Find("GameController").GetComponent<GameController1>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject.Find("GameController").GetComponent<audioController>().BallHitSound();
    //     if (other.collider.name == "GoalAI")
    //     {   // AI has Scored a Goal
    //         RPC_AIGoal();
    //         //RPC_AIGoal();
    //         StartCoroutine("AIGoal");
    //     }
    //     if (other.collider.name == "GoalP")
    //     {   // Player has Scored a Goal
    //         RPC_PGoal();
    //         //RPC_PGaol();
    //         StartCoroutine("PGoal");
    //     }
    }
    void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed); //Speed Limit
    }
    void Update()
    {
        var lag = rb.transform.position - BallMovement3.receivePos;
        if(lag.magnitude>10)
            transform.position=BallMovement3.receivePos;  //teleport
        else if (lag.magnitude>1.5f){
            //rb.AddRelativeForce(lag.normalized * speed, ForceMode2D.Force);
            // else if(lag.magnitude>1)
            transform.position=Vector2.MoveTowards(transform.position,BallMovement3.receivePos,0.2f);
            Debug.Log(rb.velocity);
            Debug.Log(rb.transform.position);
            Debug.Log(BallMovement3.receivePos);
            }
        if((rb.velocity-BallMovement3.receivedVel).magnitude>0.1)
        rb.velocity=BallMovement3.receivedVel;
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
    public IEnumerator playerReset(){
        InGoal=true;
        yield return new WaitForSeconds(0.7f);
        InGoal=false;
    }
}