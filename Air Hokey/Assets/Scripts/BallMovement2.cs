using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;

public class BallMovement2 : MonoBehaviour
{
    private int GamePoint = 5;//point to win the Game
    private float speed = 9;
    Rigidbody2D rb;
    private int ScoreAI, ScoreP;
    public GameObject AIScore, PScore;//UI for Score Display
    private GameController gc;
    public Collider2D Divider;//Divider between Player and AI
    PhotonView PV;
    void Start()
    {
        PV=GetComponent<PhotonView>();
        //Ball not to Collide with Divider between Player and AI
        Physics2D.IgnoreCollision(Divider, gameObject.GetComponent<Collider2D>(), true);
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        if (SceneScript.Difficulty == 2)
            speed = 11;
        if (SceneScript.Difficulty == 3)
            speed = 13;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject.Find("GameController").GetComponent<audioController>().BallHitSound();
        if (other.collider.name == "GoalAI")
        {   // AI has Scored a Goal
            PV.RPC("RPC_AIGoal",RpcTarget.All);
        }
        else if (other.collider.name == "GoalP")
        {   // Player has Scored a Goal
            PV.RPC("RPC_PGoal",RpcTarget.All);
        }
    }
    void FixedUpdate()
    {
        if(PhotonNetwork.IsMasterClient)
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed); //Speed Limit
    }
    void Update()
    {
        if (ScoreAI == GamePoint || ScoreP == GamePoint) //Game ENDED
        {
            if (ScoreAI == GamePoint) //AI Won
            {
                gc.Finish("Player 2 Won");
                GameObject.Find("GameController").GetComponent<audioController>().LoseSound();
                ScoreAI = 0;
            }
            else // Player Won
            {
                gc.Finish("Player 1 Won");
                GameObject.Find("GameController").GetComponent<audioController>().WinSound();
                ScoreP = 0;
            }
        }
    }
    [PunRPC]
    private void RPC_AIGoal(){
            GameObject.Find("GameController").GetComponent<audioController>().GoalSound();
            StartCoroutine("AIGoal");
            rb.position = new Vector2(-12f, -12f);//out of screen
            rb.velocity = new Vector2(0f, 0f);
            ScoreAI += 1;
            AIScore.GetComponent<TMPro.TextMeshProUGUI>().text = ScoreAI.ToString();
    }
    [PunRPC]
    private void RPC_PGaol(){
            GameObject.Find("GameController").GetComponent<audioController>().GoalSound();
            StartCoroutine("PGoal");
            rb.position = new Vector2(-10f, -10f);
            rb.velocity = new Vector2(0f, 0f);
            ScoreP += 1;
            PScore.GetComponent<TMPro.TextMeshProUGUI>().text = ScoreP.ToString();
    }

    private IEnumerator AIGoal()//Reset Ball Position with a bit Delay
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.2f);
        rb.transform.Rotate(new Vector3(0f,0f,0f));
        rb.position = new Vector2(0f, -0.5f);
        AI.FirstHit = false;
    }
    private IEnumerator PGoal()//Reset Ball Position with a bit Delay
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.2f);
        rb.transform.Rotate(new Vector3(0f,0f,0f));
        rb.position = new Vector2(0f, 0.5f);
        AI.FirstHit = false;
    }
}