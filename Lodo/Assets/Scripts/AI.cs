using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Pucks Puck;
    int[] safepos = {1,9,14,22,27,35,40,48};
    GameController gc;
    void Start()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameController>();
        DiceRoll.AIroller += AIreset;
        Puck = gameObject.GetComponent<Pucks>();
    }
    IEnumerator SelectRandom(float temprory)
    {
        temprory += Random.Range(0.1f, 0.2f);
        yield return new WaitForSeconds(temprory);
        Puck.TryMove();
    }
    void AIreset()
    {
        if (Puck.player == DiceRoll.chance)
        {
            if (!Puck.inHome)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (gc.AllPucks[i].player == Puck.player)
                        continue;
                    if (Puck.dice + Puck.place + 1 == gc.AllPucks[i].place)
                    {
                        if (gc.AllPucks[i].place > 52)
                            continue;
                        Puck.TryMove();
                        return;
                    }
                    if(Puck.count>45){
                        StopCoroutine("SelectRandom");
                        StartCoroutine("SelectRandom",0.2f);
                        return;
                    }
                    if ((Puck.place - gc.AllPucks[i].place < 5) || (Puck.dice + Puck.place - gc.AllPucks[i].place > -4))
                        {
                            if(gameObject.GetComponent<BoxCollider>().isTrigger)
                            continue;
                            StopCoroutine("SelectRandom");
                            StartCoroutine("SelectRandom",0.1f);
                            return;
                        }
                }
            }
            else if(Puck.dice==5)
            {
                StopCoroutine("SelectRandom");
                StartCoroutine("SelectRandom",0);
                return;
            }
            for(int j=0;j<8;j++){
                    if (Puck.dice + Puck.place + 1 == safepos[j]-1){
                        StopCoroutine("SelectRandom");
                        StartCoroutine("SelectRandom",0);
                        return;
                    }
                }
            //Debug.Log("AI Reset");
            StopCoroutine("SelectRandom");
            StartCoroutine("SelectRandom",0.3f);
        }
    }
}