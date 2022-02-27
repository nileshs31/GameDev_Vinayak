using UnityEngine;
public class AI : MonoBehaviour
{
    private float offsetXFromTarget,AIspeed = 5;
    public Rigidbody2D BallBody;
    private Rigidbody2D rb;
    private Vector2 Ballpos,Targetpos;
    private bool isFirstTimeInOpponentsHalf=true; //if ball is in Player Side for first time
    public static bool FirstHit=false; //the AI Hit the Ball no need to follow it with much speed
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(SceneScript.Difficulty==2)
        AIspeed=10;
        if(SceneScript.Difficulty==3)
        AIspeed=15;        
    }
    
    void FixedUpdate()
    {
        Ballpos=BallBody.position;
        if (Ballpos.y<0)//Ball is in Player Side
            {
            FirstHit=false;
            if (isFirstTimeInOpponentsHalf)
                {
                    isFirstTimeInOpponentsHalf = false;
                    offsetXFromTarget = Random.Range(-1f, 1f);
                }
            Targetpos = new Vector2(Mathf.Clamp(Ballpos.x + offsetXFromTarget,-2.2f,2.2f),4.3f);
            }
        else//ball is in AI side
            {
                isFirstTimeInOpponentsHalf = true;
                if(Ballpos.y>rb.position.y){//Ball is Behind the AI
                    if(Ballpos.x>0){
                        Targetpos=new Vector2(Ballpos.x-0.4f,Ballpos.y+0.5f);
                    }
                    else{
                        Targetpos=new Vector2(Ballpos.x+0.4f,Ballpos.y+0.5f);
                    }
                }
                else
                Targetpos=Ballpos;
            }
        if(FirstHit){//slow speed
            rb.MovePosition(Vector2.MoveTowards(rb.position,Targetpos,AIspeed * 0.7f * Time.fixedDeltaTime));
        }
        else{
        rb.MovePosition(Vector2.MoveTowards(rb.position,Targetpos,AIspeed * Time.fixedDeltaTime));
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {//checking if AI hit the Ball
        if(Ballpos.y>0)
        if((other.collider.name=="Ball") && !FirstHit){
            FirstHit=true;
        }
    }
}