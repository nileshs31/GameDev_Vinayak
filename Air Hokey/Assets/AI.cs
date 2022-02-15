using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float AIspeed =  10;
    public Rigidbody2D BallBody;
    private Rigidbody2D rb;
    private Vector2 Ballpos;
    private Vector2 Targetpos;
    private Vector2 Movepos;
    static int Ball=0;
    private float offsetXFromTarget;
    private bool isFirstTimeInOpponentsHalf=true;
    // 0 is ball span other// 1 ball in span here// 2 in game ball enter other// 3 ball comming towards// 4 ball in box
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        Ballpos=BallBody.position;
        /*switch (Ball)
        {
            case 0:
                break;
            case 1:
                Targetpos=new Vector2(Mathf.Clamp(Ballpos.x,-2.2f,2.2f),Mathf.Clamp(Ballpos.y,0.5f,4.45f));
                break;
            case 2:
                Targetpos=new Vector2(Random.Range(-2.2f,2.2f),Random.Range(0.5f,4.45f));
                break;
            case 3:
                Targetpos=new Vector2(Mathf.Clamp(Ballpos.x + Random.Range(-1f,1f),-2.2f,2.2f),Mathf.Clamp(Ballpos.y + Random.Range(-1f,1f),0.5f,4.45f));
                break;
            case 4:
                Targetpos=new Vector2(Mathf.Clamp(Ballpos.x,-2.2f,2.2f),Mathf.Clamp(Ballpos.y,0.5f,4.45f));
                break;
            default:
                break;
        }*/
            if (Ballpos.y<0)
            {
                if (isFirstTimeInOpponentsHalf)
                {
                    isFirstTimeInOpponentsHalf = false;
                    offsetXFromTarget = Random.Range(-1f, 1f);
                }

                //AIspeed * Random.Range(0.1f, 0.3f);
                Targetpos = new Vector2(Mathf.Clamp(Ballpos.x + offsetXFromTarget,-2.2f,2.2f),3.6f);
            }
            else
            {
                isFirstTimeInOpponentsHalf = true;
                Targetpos=new Vector2(Mathf.Clamp(Ballpos.x,-2.2f,2.2f),Mathf.Clamp(Ballpos.y,0.5f,4.45f));
            }

        //rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition,movementSpeed * Time.fixedDeltaTime));
        rb.MovePosition(Vector2.MoveTowards(rb.position,Targetpos,AIspeed * Random.Range(0.1f, 0.3f) * Time.fixedDeltaTime));
    }

    private void LateUpdate()
    {
        if(Ballpos.y>0)
        Ball=4;
        else if(Ballpos.y>-2)
        Ball=3;
        else
        Ball=2;        
    }
}