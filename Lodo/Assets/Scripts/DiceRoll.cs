using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public GameObject[] DicePlaceHolder;
    public Sprite[] DiceNumber;
    public static int chance = 0; //Which Player's Chance
    public static int turn = 0; //Dice or Puck
    public static int[] AllIN; // no. of puck in Home
    public static Action<int> Rolled; //Updating dice value to all pucks
    public static bool OnlyOne = true; //when two puck on same point
    int[] BadLuck; // Six not comming

    void Start()
    {
        DicePlaceHolder[0].SetActive(true);
        gameObject.transform.position = DicePlaceHolder[0].transform.position;
        AllIN = new int[4];
        BadLuck = new int[4];
        // for (int i = 0; i < 4; i++)
        // {
        //     AllIN[i] = 0;
        // }
    }
    private void OnMouseDown()
    {
        if (turn == 0) //Dice turn
            gameObject.GetComponent<Animator>().enabled = true;
    }
    public void OnMouseUp()
    {
        if (turn == 0) //Dice turn
            StartCoroutine("Roll");
    }
    IEnumerator Roll()
    {
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
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Animator>().enabled = false;
        if (Rolled != null)
        {
            Rolled(temp);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = (DiceNumber[temp]);
        if (temp == 5 || AllIN[chance] != 0)
        {
            turn = 1; //Playable
        }
        else
        { //No Available Moves
            yield return new WaitForSeconds(1f);
            changeTurn();
        }
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