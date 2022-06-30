using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Pucks : MonoBehaviour
{
    int player = 0, dice, count = 0, place;
    bool inHome = true;
    Vector3[] unit;
    Vector2 Origin;
    public GameObject[] PublicRoute;
    //GameObject[] PrivateRoute;
    bool alive = false;
    GameObject Parent;
    public static Action OverlapFixed;
    private void OnEnable()
    {
        DiceRoll.Rolled += Dicenumber;
        OverlapFixed += OverlapFix;
    }
    void Start()
    {
        //PrivateRoute=new GameObject[6];
        Origin = transform.position;
        if (gameObject.name.Contains("Red"))
        {
            player = 0;
            Parent = GameObject.Find("RedIn");
        }
        else if (gameObject.name.Contains("Blue"))
        {
            player = 1;
            place = 13;
            Parent = GameObject.Find("BlueIn");
        }
        else if (gameObject.name.Contains("Green"))
        {
            player = 2;
            place = 26;
            Parent = GameObject.Find("GreenIn");
        }
        else if (gameObject.name.Contains("Yellow"))
        {
            player = 3;
            place = 39;
            Parent = GameObject.Find("YellowIn");
        }
        for (int i = 0; i < 6; i++)
        {
            PublicRoute[52 + i] = Parent.transform.GetChild(i).gameObject;
            Debug.Log(player + " " + PublicRoute[52 + i].transform.position);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name.Contains("Circle"))
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
        else if (other.gameObject.GetComponent<Pucks>().player != player)
        {
            if (!alive && other.gameObject.GetComponent<Pucks>().alive)
            {
                //DiceRoll.OnlyOne=false;
                Debug.Log("dead " + gameObject.name);
                transform.position = Origin;
                switch (player)
                {
                    case 0:
                        place = 0;
                        break;
                    case 1:
                        place = 13;
                        break;
                    case 2:
                        place = 26;
                        break;
                    case 3:
                        place = 39;
                        break;
                }
                count = 0;
                inHome = true;
                DiceRoll.AllIN[player] -= 1;
            }
            if (alive)
                StartCoroutine("MakeAlive");
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    void OnMouseUp()
    {
        Debug.Log("Touch " + gameObject.name);
        if (DiceRoll.turn == 0 || DiceRoll.chance != player || (DiceRoll.AllIN[player] == 1 && dice != 5))
        {
            return;
        }
        else if (inHome && dice == 5)
        {
            Debug.Log("free " + gameObject.name);
            transform.position = PublicRoute[place].transform.position;
            GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
            inHome = false;
            DiceRoll.AllIN[player] += 1;
            count += 1;
        }
        else if (DiceRoll.OnlyOne && !inHome && count + dice < 57)
        {
            DiceRoll.OnlyOne = false;
            Debug.Log("Moves " + gameObject.name);
            StartCoroutine("MovePlayer");
        }
    }
    void Dicenumber(int value)  //Automatically Triggered by DiceRoll
    {
        OverlapIssue();
        if (DiceRoll.chance != player)
            return;
        DiceRoll.tempdice = DiceRoll.AllIN[player];
        //DiceRoll.tempdice2=DiceRoll.AllIN[player];
        dice = value;
        if (!inHome && DiceRoll.AllIN[player] > 0 && dice != 5)
        {
            if (count + dice > 57)
                StartCoroutine("ChangingTurn");
            else
                StartCoroutine("AutoRunning");
        }
        // else if (dice == 5 && DiceRoll.AllIN[player] == 0 && gameObject.name.Contains("1"))
        // {
        //     Debug.Log("Auto " + gameObject.name);
        //     StartCoroutine("AutoRun");
        // }
        // else if (DiceRoll.AllIN[player] == 1 && !inHome && dice != 5 && count+dice<57)
        // {
        //     Debug.Log("Automove " + gameObject.name);
        //     StartCoroutine("MovePlayer");
        // }
        //Debug.Log(dice);
    }
    IEnumerator MovePlayer()
    {
        if(OverlapFixed != null){
            OverlapFixed();
        }
        for (int i = 0; i <= dice; i++)
        {
            if (i == dice)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                alive = true;
            }
            place += 1;
            count += 1;
            if (count == 52)
                place = 52;
            else if (place == 52)
                place = 0;
            transform.position = PublicRoute[place].transform.position;
            //Debug.Log(transform.position);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.3f);
        if (count == 57)
        {
            DiceRoll.Completed[player] += 1;
            DiceRoll.AllIN[player] -= 1;
            GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
            gameObject.GetComponent<Pucks>().enabled = false;
        }
        else if (dice == 5 || !alive)
            GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
        else
            GameObject.Find("Dice").GetComponent<DiceRoll>().changeTurn();
        alive = false;
        DiceRoll.OnlyOne = true;
    }
    IEnumerator MakeAlive()
    {
        yield return new WaitForSeconds(0.2f);
        alive = false;
    }
    IEnumerator AutoRun()
    {
        yield return new WaitForSeconds(0.15f);
        //Debug.Log();
        transform.position = PublicRoute[place].transform.position;
        GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
        inHome = false;
        DiceRoll.AllIN[player] += 1;
        count += 1;
    }
    IEnumerator ChangingTurn()
    {
        yield return new WaitForSeconds(0.3f);
        DiceRoll.tempdice -= 1;
        if (DiceRoll.tempdice == 0)
            GameObject.Find("Dice").GetComponent<DiceRoll>().changeTurn();
    }
    IEnumerator AutoRunning()
    {
        DiceRoll.tempdice2 += 1;
        yield return new WaitForSeconds(0.5f);
        if (DiceRoll.tempdice2 == 1)
            StartCoroutine("MovePlayer");
    }
    void OverlapIssue(){
        if(player==DiceRoll.chance)
            gameObject.GetComponent<BoxCollider>().enabled=true;
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled=false;
        }
    }
    void OverlapFix(){
        gameObject.GetComponent<BoxCollider>().enabled=true;
    }
}