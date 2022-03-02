using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody rb;
    public float Speed = 10;
    public GameObject Dead;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rb.AddForce(new Vector3(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            rb.AddForce(new Vector3(0,0,Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime));
        }
    }
    void OnCollisionEnter(Collision Hit)
    {
        if (Hit.collider.tag == "Obstacle")
        {
            Time.timeScale = 0;
            Dead.SetActive(true);
        }
    }
}
