using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Pucks Puck;
    int[] safepos = { 1, 9, 14, 22, 27, 35, 40, 48 };
    GameController gc;
    private void OnEnable() {
        Pucks.AIStop+=StopAll;
    }
    private void OnDisable() {
        Pucks.AIStop-=StopAll;
    }
    void Start()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameController>();
        DiceRoll.AIroller += AIreset;
        Puck = gameObject.GetComponent<Pucks>();
    }
    IEnumerator SelectRandom(float temprory)    //Madatory wait temp+(1-2)
    {
        temprory += Random.Range(0.1f, 0.2f);
        yield return new WaitForSeconds(temprory);
        Puck.TryMove();
    }
    void AIreset()
    {
        if (Puck.player != DiceRoll.chance)
            return;
        if (!Puck.inHome)
        {
            for (int i = 0; i < 16; i++)    //Checking evey other puck place
            {
                if (gc.AllPucks[i].player == Puck.player)
                    continue;
                if (Puck.dice + Puck.place + 1 == gc.AllPucks[i].place)     //Able to kill
                {
                    if (gc.AllPucks[i].place > 52)  //Unable
                        continue;
                    Puck.TryMove(); //JustKILL
                    return;     //DONE
                }
                if (Puck.count > 45)    //Close to Target
                {
                    //StopCoroutine("SelectRandom");
                    StartCoroutine("SelectRandom", 0.2f);   // Wait 2
                    return; //DONE
                }   // Just in Front of Other Puck          OR      Can Get close behind other puck
                if ((Puck.place - gc.AllPucks[i].place < 5) || (Puck.dice + Puck.place - gc.AllPucks[i].place > -4))
                {
                    if (gameObject.GetComponent<BoxCollider>().isTrigger)   //On Safe Position
                        continue;
                    //StopCoroutine("SelectRandom");
                    StartCoroutine("SelectRandom", 0.2f);   //Wait 1
                    return; //DONE
                }
            }
        }
        else if (Puck.dice == 5)    //InHome and got 6
        {
            //StopCoroutine("SelectRandom");
            StartCoroutine("SelectRandom", 0);  //Wait 0
            return; //DONE
        }
        for (int j = 0; j < 8; j++)     //Checking if can get in Safe Position
        {
            if (Puck.dice + Puck.place + 1 == safepos[j] - 1)
            {
                //StopCoroutine("SelectRandom");
                StartCoroutine("SelectRandom", 0);  //Wait 0
                return; //DONE
            }
        }
        //Debug.Log("AI Reset");
        //StopCoroutine("SelectRandom");
        StartCoroutine("SelectRandom", 0.4f);   //Nothing just wait to see if other puck are running
    }
    public void StopAll(){
        StopAllCoroutines();
    }
}