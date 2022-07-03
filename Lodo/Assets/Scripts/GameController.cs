using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] SafePointes,WinOverlay,BluePucks,YellowPucks;
    public int[] AllIN,Completed;
    public static int tempdice=1,tempdice2=0;

    void Start()
    {
        AllIN = new int[4];
        Completed = new int[4];
        switch(TitleManager.playercount){
            case 2: Completed[1]=4;
                    WinOverlayer(1);
                    //BlueRemover();
                    Completed[3]=4;
                    WinOverlayer(3);
                    //YellowRemover();
                    break;
            case 3: Completed[3]=4;
                    WinOverlayer(3);
                    //YellowRemover();
                    break;
            default: break;
        }
    }
    private void OnEnable() {
        Pucks.OverlapFixed += SafePointFix;
        DiceRoll.Rolled += SafePointRefix;
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
    public void WinOverlayer(int p,int pos = 0){
        WinOverlay[p].SetActive(true);
        if(pos==0)
        WinOverlay[p].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
        else
        WinOverlay[p].GetComponentInChildren<TMPro.TextMeshProUGUI>().text+=pos;
    }
    public void BlueRemover(){
        for(int i=0; i<4;i++){
            Destroy(BluePucks[i]);
        }
    }
    public void YellowRemover(){
        for(int i=0; i<4;i++){
            Destroy(YellowPucks[i]);
        }
    }
}
