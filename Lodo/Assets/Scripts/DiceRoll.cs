using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public GameObject[] DicePlaceHolder;
    public Sprite[] DiceNumber;
    public static int chance; //Which Player's Chance
    public static int turn = 0; //Dice or Puck
    public static int[] AllIN; // no. of puck in Home
    public static int[] Completed;
    public static Action<int> Rolled; //Updating dice value to all pucks
    public static bool OnlyOne = true; //when two puck on same point
    int[] BadLuck; // Six not comming
    public static int tempdice=1,tempdice2=0;

    void Start()
    {
        chance = 0;
        DicePlaceHolder[0].SetActive(true);
        gameObject.transform.position = DicePlaceHolder[0].transform.position;
        AllIN = new int[4];
        Completed = new int[4];
        BadLuck = new int[4];
        // for (int i = 0; i < 4; i++)
        // {
        //     AllIN[i] = 0;
        // }
    }
    void OnMouseDown()
    {
        if (turn == 0) //Dice turn
            gameObject.GetComponent<Animator>().enabled = true;
    }
    void OnMouseUp()
    {
        if (turn == 0) //Dice turn
        {
            tempdice=1;
            tempdice2=0;
            StartCoroutine("Roll");
        }
    }
    IEnumerator Roll()
    {
        Debug.Log(chance);
        var temp = UnityEngine.Random.Range(0, 6);
        if (temp == 5)
            BadLuck[chance] = 0; //player got six
        else
            BadLuck[chance] += 1; //player didn't got six
        if (AllIN[chance] < 3 && BadLuck[chance] > 1 && temp != 5) //Bad Luck Increased
        {
            for (int i = 0; i < BadLuck[chance]; i++)
            {
                temp = UnityEngine.Random.Range(0, 6);
                if (temp == 5)
                    i = BadLuck[chance];
            }
            if(temp==5)
            BadLuck[chance] = -2;
        }
        yield return new WaitForSeconds(0.31f);
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = (DiceNumber[temp]);
        Debug.Log(chance);
        if (Rolled != null)
        {
            Rolled(temp);
        }
        if (temp == 5 || AllIN[chance] != 0)
        {
            turn = 1; //Playable
        }
        else
        { //No Available Moves
            yield return new WaitForSeconds(0.31f);
            changeTurn();
        }
        Debug.Log(chance);
    }
    public void changeTurn()
    {
        turn = 0; //Dice Turn
        DicePlaceHolder[chance].SetActive(false);
        chance += 1; // next player chance
        if (chance == 4)
            chance = 0;
        gameObject.transform.position = DicePlaceHolder[chance].transform.position;
        DicePlaceHolder[chance].SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().sprite = (DiceNumber[0]);
    }
    public void ReThrow()
    {
        //Debug.Log("Roll Again");
        turn = 0;
    }
}