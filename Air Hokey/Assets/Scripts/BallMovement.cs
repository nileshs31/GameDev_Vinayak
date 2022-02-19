using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BallMovement : MonoBehaviour
{
    private int GamePoint=1;
    private float speed=12;
    Rigidbody2D rb;
    private int ScoreAI,ScoreP;
    public GameObject AIScore,PScore;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
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
            GameObject.Find("GameController").GetComponent<GameController>().Finish("You Lose");
            GameObject.Find("GameController").GetComponent<audioController>().LoseSound();
            ScoreAI=0;
            }
            else{
            GameObject.Find("GameController").GetComponent<GameController>().Finish("You Win");
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