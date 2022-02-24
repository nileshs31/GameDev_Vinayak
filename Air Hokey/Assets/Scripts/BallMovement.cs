using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BallMovement : MonoBehaviour
{
    private int GamePoint=5;
    private float speed=8;
    Rigidbody2D rb;
    private int ScoreAI,ScoreP;
    public GameObject AIScore,PScore;
    private GameController gc;
    public Collider2D Divider;
    void Start()
    {
        Physics2D.IgnoreCollision(Divider,gameObject.GetComponent<Collider2D>(),true);
        gc=GameObject.Find("GameController").GetComponent<GameController>();
        rb=GetComponent<Rigidbody2D>();
        if(SceneScript.Difficulty==2)
        speed=13;
        if(SceneScript.Difficulty==3)
        speed=17;   
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject.Find("GameController").GetComponent<audioController>().BallHitSound();
        if(other.collider.name=="GoalAI"){
        GameObject.Find("GameController").GetComponent<audioController>().GoalSound();
        StartCoroutine("AIGoal");
        rb.position=new Vector2(10f,10f);
        rb.velocity=new Vector2(0f,0f);
        ScoreAI+=1;
        AIScore.GetComponent<TMPro.TextMeshProUGUI>().text=ScoreAI.ToString();
        }
        else if(other.collider.name=="GoalP"){
        GameObject.Find("GameController").GetComponent<audioController>().GoalSound();
        StartCoroutine("PGoal");
        rb.position=new Vector2(10f,10f);
        rb.velocity=new Vector2(0f,0f);
        ScoreP+=1;
        PScore.GetComponent<TMPro.TextMeshProUGUI>().text=ScoreP.ToString();
        }
    }
    void FixedUpdate()
    {        
        rb.velocity = Vector2.ClampMagnitude(rb.velocity,speed);
    }
    void Update()
    {
        if(ScoreAI==GamePoint || ScoreP==GamePoint){
            if(ScoreAI==GamePoint){
            if(SceneScript.Difficulty==1)
            gc.Finish("HAHAHA! Loser");
            else if(SceneScript.Difficulty==2)
            gc.Finish("You Gotta DO Better Kid");
            else if(SceneScript.Difficulty==3)
            gc.Finish("You Can't Live With Your own Failure");
            else
            gc.Finish("Player 2 Won");
            GameObject.Find("GameController").GetComponent<audioController>().LoseSound();
            ScoreAI=0;
            }
            else{
            if(SceneScript.Difficulty==1)
            gc.Finish("Puff! That was Easy Win");
            else if(SceneScript.Difficulty==2)
            gc.Finish("Not Bad!");
            else if(SceneScript.Difficulty==3)
            gc.Finish("UNPOSSIBLE!");
            else
            gc.Finish("Player 1 Won");
            GameObject.Find("GameController").GetComponent<audioController>().WinSound();
            ScoreP=0;
            }
        }
    }

    private IEnumerator AIGoal(){
        yield return new WaitForSeconds(1f);
        rb.position=new Vector2(0f,0f);     
    }
    private IEnumerator PGoal(){
        yield return new WaitForSeconds(1f);
        rb.position=new Vector2(0f,0f);     
    }    
}