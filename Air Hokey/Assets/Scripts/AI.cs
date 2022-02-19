using UnityEngine;

public class AI : MonoBehaviour
{
    public float offsetXFromTarget,AIspeed = 20;
    public Rigidbody2D BallBody;
    private Rigidbody2D rb;
    private Vector2 Ballpos,Targetpos;
    private bool isFirstTimeInOpponentsHalf=true;    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        Ballpos=BallBody.position;
        if (Ballpos.y<0)
            {
            if (isFirstTimeInOpponentsHalf)
                {
                    isFirstTimeInOpponentsHalf = false;
                    offsetXFromTarget = Random.Range(-1f, 1f);
                }
            Targetpos = new Vector2(Mathf.Clamp(Ballpos.x + offsetXFromTarget,-2.2f,2.2f),3.6f);
            }
        else
            {
                isFirstTimeInOpponentsHalf = true;
                Targetpos=new Vector2(Mathf.Clamp(Ballpos.x,-2.2f,2.2f),Mathf.Clamp(Ballpos.y,0.5f,4.45f));
            }
        rb.MovePosition(Vector2.MoveTowards(rb.position,Targetpos,AIspeed * Random.Range(0.1f, 0.3f) * Time.fixedDeltaTime));
    }
}