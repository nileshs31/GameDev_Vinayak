using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] SafePointes;
    private void OnEnable() {
        Pucks.OverlapFixed += SafePointFix;
        DiceRoll.Rolled += SafePointRefix;
    }
    void Start()
    {
        
    }

    void SafePointFix(){
        for(int i=0; i<8;i++){
            SafePointes[i].GetComponent<BoxCollider>().enabled=true;
        }
    }
    void SafePointRefix(int a){
        for(int i=0; i<8;i++){
            SafePointes[i].GetComponent<BoxCollider>().enabled=false;
        }
    }
}
