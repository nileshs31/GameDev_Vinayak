using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Rigidbody2D Ball;
    private Rigidbody2D rb;
    private Vector2 Ballpos;
    private Vector2 Targetpos;
    private Vector2 Movepos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ballpos=Ball.position;
        Targetpos=new Vector2(Mathf.Clamp(Ballpos.x,-2.2f,2.2f),Mathf.Clamp(Ballpos.y,0.5f,4.45f));
        rb.MovePosition(Targetpos);
    }
}