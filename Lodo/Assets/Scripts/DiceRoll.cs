using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public GameObject[] DicePlaceHolder;
    public Sprite[] DiceNumber;
    public static int chance; //Which Player's Chance
    public static int turn; //Dice or Puck
    public static Action<int> Rolled; //Updating dice value to all pucks
    public static bool OnlyOne; //when two puck on same point
    int[] BadLuck; // Six not comming
    public static int tempdice, tempdice2;
    GameController gc;
    public static bool[] AItracker;
    public static Action AIroller;
    private void OnEnable()
    {
        Rolled = null;
        AIroller = null;
    }
    void Start()
    {
        OnlyOne = true;
        tempdice = 1;
        tempdice2 = 0;
        turn = 0;
        gc = GameObject.Find("GameManager").GetComponent<GameController>();
        chance = 0;
        DicePlaceHolder[0].SetActive(true);
        gameObject.transform.position = DicePlaceHolder[0].transform.position;
        BadLuck = new int[4];
        AItracker = new bool[4];
        AItracker[0] = TitleManager.AIred;
        AItracker[1] = TitleManager.AIblue;
        AItracker[2] = TitleManager.AIgreen;
        AItracker[3] = TitleManager.AIyellow;
        if (AItracker[chance])
        {
            StartCoroutine("DelayedSpin");
        }
    }
    void OnMouseDown()
    {
        if (!AItracker[chance])
            Spin();
    }
    void OnMouseUp()
    {
        if (!AItracker[chance])
            SpinOff();
    }
    IEnumerator Roll()
    {
        var temp = UnityEngine.Random.Range(0, 6);
        if (temp == 5)
            BadLuck[chance] = 0; //player got six
        else    //BadLuck Code
        {
            BadLuck[chance] += 1;
            if (gc.AllIN[chance] + gc.Completed[chance] < 4 && BadLuck[chance] > 1) //Bad Luck Loop
            {
                for (int i = 0; i < BadLuck[chance]; i++)
                {
                    temp = UnityEngine.Random.Range(0, 6);
                    if (temp == 5)
                    {
                        break;
                    }
                }
            }
        }
        if(temp==5){
            if (BadLuck[chance] != -2)
                BadLuck[chance] = -4;
            else if (BadLuck[chance] == -4)
            {
                temp = UnityEngine.Random.Range(0, 5);
            }
            else
                BadLuck[chance] = -2;
        }
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = (DiceNumber[temp]);
        if (Rolled != null) //Autorun in Puck script
        {
            Rolled(temp);
        }
        if (temp == 5 || gc.AllIN[chance] != 0)
        {
            //Debug.Log(gc.AllIN[chance]);
            turn = 1; //Playable
        }
        else
        { //No Available Moves
            yield return new WaitForSeconds(0.31f);
            changeTurn();
        }
        if (AIroller != null && AItracker[chance]) //Autorun in AI script
        {
            //Debug.Log("Calling Ai Roller");
            StartCoroutine("DelayedAI");
        }
    }
    public void changeTurn()
    {
        //Debug.Log("Changed turn ");
        turn = 0; //Dice Turn
        DicePlaceHolder[chance].SetActive(false);
    Turner:
        chance += 1; // next player chance
        if (chance == 4)
            chance = 0;
        if (gc.Completed[chance] == 4)
        {
            goto Turner;
        }
        gameObject.transform.position = DicePlaceHolder[chance].transform.position;
        DicePlaceHolder[chance].SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().sprite = (DiceNumber[0]);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        //Debug.Log(chance);
        if (AItracker[chance])
        {
            StartCoroutine("DelayedSpin");
        }
    }
    public void ReThrow()
    {
        //Debug.Log("Roll Again");
        turn = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = (DiceNumber[0]);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (AItracker[chance])
        {
            StartCoroutine("DelayedSpin");
        }
    }
    public void Spin()
    {
        if (turn == 0) //Dice turn
            gameObject.GetComponent<Animator>().enabled = true;
    }
    public void SpinOff()
    {
        if (turn == 0) //Dice turn
        {
            tempdice = 1;
            tempdice2 = 0;
            StartCoroutine("Roll");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    IEnumerator DelayedSpin()
    {
        yield return new WaitForSeconds(0.25f);
        Spin();
        SpinOff();
    }
    IEnumerator DelayedAI()
    {
        yield return new WaitForSeconds(0.1f);
        AIroller();
    }
}