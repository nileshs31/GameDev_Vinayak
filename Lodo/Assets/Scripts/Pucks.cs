using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Pucks : MonoBehaviour
{
    public int player = 0, dice, count = 0, place;
    public bool inHome = true;
    Vector2 Origin;
    public GameObject[] PublicRoute;
    public bool alive = false; //To Detect Collisions in MovePlayer
    GameObject Parent; //For Routing Last Route of each color
    public static Action OverlapFixed;
    GameController gc;
    private void OnEnable() {
        OverlapFixed=null;
    }
    void Start()
    {
        DiceRoll.Rolled += Dicenumber;
        OverlapFixed += OverlapFix;
        gc=GameObject.Find("GameManager").GetComponent<GameController>();
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
        for (int i = 0; i < 6; i++) //Setting Last Routes for each puck
        {
            PublicRoute[52 + i] = Parent.transform.GetChild(i).gameObject;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name.Contains("Circle")) //Safe Position
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true; //Dont Take Collisions
        }
        else if (other.gameObject.GetComponent<Pucks>().player != player)
        {
            if (!alive && other.gameObject.GetComponent<Pucks>().alive) //Player to Die
            {
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
                gc.AllIN[player] -= 1;
            }
            if (alive) //Who Killed
                StartCoroutine("MakeAlive");
        }
        // else //Overlap with same player or Safe Position
        // {
        //     gameObject.GetComponent<BoxCollider>().isTrigger = true;
        //     other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        // }
    }
    void OnMouseUp()
    {
        if(!DiceRoll.AItracker[player])
        TryMove();
    }
    void Dicenumber(int value)  //Automatically Triggered by DiceRoll
    {
        OverlapIssue(); //Disable all Colliders except Player whose chance is this
        if (DiceRoll.chance != player)
            return;
        DiceRoll.tempdice = gc.AllIN[player];
        dice = value;
        if (!inHome && gc.AllIN[player] > 0 && dice != 5)
        {
            if (count + dice >= 57)
                StartCoroutine("ChangingTurn");
            else
                StartCoroutine("AutoRunning");
        }
        else if (!inHome && gc.AllIN[player]+gc.Completed[player]==4 && dice == 5){ //All Out
            if (count + dice >= 57)
                StartCoroutine("ChangingTurn");
            else
                StartCoroutine("AutoRunning");
        }
        DiceRoll.OnlyOne = true;
    }
    IEnumerator MovePlayer()
    {
        if(OverlapFixed != null){
            OverlapFixed(); //Renabling Colliders
        }
        for (int i = 0; i <= dice; i++)
        {
            if (i == dice) //Detect Collision
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                alive = true;
            }
            place += 1;
            count += 1;
            if (count == 52) //Start Personal Route
                place = 52;
            else if (place == 52)
                place = 0;
            transform.position = PublicRoute[place].transform.position;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        if (count == 57) //Reached Destination
        {
            gc.Completed[player] += 1;
            gc.AllIN[player] -= 1;
            DiceRoll.Rolled -= Dicenumber;
            OverlapFixed -= OverlapFix;
            if(gc.Completed[player]==4){
            gc.WinOverlayer(player);
            GameObject.Find("Dice").GetComponent<DiceRoll>().changeTurn();
            }
            else
            GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
            gameObject.GetComponent<Pucks>().enabled = false;
        }
        else if (dice == 5 || !alive) //Manual Choose in Six
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
        transform.position = PublicRoute[place].transform.position;
        GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
        inHome = false;
        gc.AllIN[player] += 1;
        count += 1;
    }
    IEnumerator ChangingTurn()
    {
        Debug.Log(gameObject.name +" Routine "+player);
        yield return new WaitForSeconds(0.25f);
        DiceRoll.tempdice -= 1;
        if (DiceRoll.tempdice == 0){
            GameObject.Find("Dice").GetComponent<DiceRoll>().changeTurn();
            Debug.Log(gameObject.name + "Call Change Turn "+player);
        }
    }
    IEnumerator AutoRunning()
    {
        DiceRoll.tempdice2 += 1;
        yield return new WaitForSeconds(0.25f);
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
    public void TryMove(){
        if (DiceRoll.turn == 0 || DiceRoll.chance != player || (gc.AllIN[player] == 1 && dice != 5))
        {
            Debug.Log(gameObject.name + "Cantt" + DiceRoll.turn);
            return;
        }
        else if (!inHome && gc.AllIN[player]==1 && dice == 5 && gc.Completed[player]==3)
            {
                Debug.Log(gameObject.name + "Cantt");
                return;
            }
        else if (DiceRoll.OnlyOne && inHome && dice == 5) // Choose player to deploy in feild
        {
            Debug.Log(gameObject.name + "Out of home");
            DiceRoll.OnlyOne=false;
            transform.position = PublicRoute[place].transform.position;
            GameObject.Find("Dice").GetComponent<DiceRoll>().ReThrow();
            inHome = false;
            gc.AllIN[player] += 1;
            count += 1;
        }
        else if (DiceRoll.OnlyOne && !inHome && count + dice < 57) 
        {
            Debug.Log(gameObject.name + "moving");
            DiceRoll.OnlyOne = false;//Overlap Player Select only One
            StartCoroutine("MovePlayer");
        }
    }
}