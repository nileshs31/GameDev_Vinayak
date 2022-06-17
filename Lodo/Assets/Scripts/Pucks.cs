using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Pucks : MonoBehaviour
{
    int player = 0, dice, count = 0,place;
    bool inHome = true;
    Vector3[] unit;
    Vector2 Origin;
    public GameObject[] PublicRoute;
    bool alive=false;
    private void OnEnable()
    {
        DiceRoll.Rolled += Dicenumber;
    }
    void Start()
    {
        Origin=transform.position;
        if (gameObject.name.Contains("Red"))
        {
            player = 0;
        }
        else if (gameObject.name.Contains("Blue"))
        {
            player = 1;
            place=13;
        }
        else if (gameObject.name.Contains("Green"))
        {
            player = 2;
            place=26;
        }
        else if (gameObject.name.Contains("Yellow"))
        {
            player = 3;
            place=39;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<Pucks>().player!=player){
            if(DiceRoll.OnlyOne && !alive && other.gameObject.GetComponent<Pucks>().alive){
                DiceRoll.OnlyOne=false;
                Debug.Log("dead "+gameObject.name);
                transform.position=Origin;
                switch (player)
                {
                    case 0:place=0;
                    break;
                    case 1:place=14;
                    break;
                    case 2:place=27;
                    break;
                    case 3:place=40;
                    break;
                }
                inHome = true;
                DiceRoll.AllIN[player]-=1;
            }
        if(alive)
        StartCoroutine("MakeAlive");
        }
    }
    void OnMouseUp()
    {
        Debug.Log("Touch "+gameObject.name);
        if (DiceRoll.turn == 0 || DiceRoll.chance != player || (DiceRoll.AllIN[player]==1 && dice!=5))
        {
            return;
        }
        else if (inHome && dice==5)
        {
            Debug.Log("free "+gameObject.name);
            transform.position = PublicRoute[place].transform.position;
            GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
            inHome = false;
            DiceRoll.AllIN[player] += 1;
        }
        else if (DiceRoll.OnlyOne && !inHome)
        {
            DiceRoll.OnlyOne = false;
            Debug.Log("Moves "+gameObject.name);
            StartCoroutine("MovePlayer");
        }
    }
    void Dicenumber(int value)  //Automatically Triggered by DiceRoll
    {
        if (DiceRoll.chance != player)
            return;
        dice = value;
        if(dice==5 && DiceRoll.AllIN[player]==0 && gameObject.name.Contains("1")){
            Debug.Log("Auto "+gameObject.name);
            StartCoroutine("AutoRun");
        }
        else if(DiceRoll.AllIN[player]==1 && !inHome && dice!=5){
            Debug.Log("Automove "+gameObject.name);
            StartCoroutine("MovePlayer");
        }
        //Debug.Log(dice);
    }
    IEnumerator MovePlayer()
    {
        for (int i = 0; i <= dice; i++)
        {
            if(i==dice)
            alive=true;
            transform.position = PublicRoute[place].transform.position;
            count+=1;
            place+=1;
            if(place==52)
            place=0;
            //Debug.Log(transform.position);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.3f);
        if (dice == 5 || !alive)
            GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
        else
            GameObject.Find("Dice").GetComponent<DiceRoll>().changeTurn();
        alive=false;
        DiceRoll.OnlyOne=true;
    }
    IEnumerator MakeAlive(){
        yield return new WaitForSeconds(0.2f);
        alive=false;
    }
    IEnumerator AutoRun(){
        yield return new WaitForSeconds(0.5f);
            //Debug.Log();
            transform.position = PublicRoute[place].transform.position;
            GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
            inHome = false;
            DiceRoll.AllIN[player] += 1;
    }
}
