using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] SafePointes,WinOverlay,BluePucks,YellowPucks,GreenPucks;
    public GameObject Menu;
    public int[] AllIN,Completed;
    public Pucks[] AllPucks;
    int screen=0;
    void Start()
    {
        AllIN = new int[4];
        Completed = new int[4];
        switch(TitleManager.playercount){
            case 2: Completed[1]=4;
                    WinOverlayer(1);
                    Completed[3]=4;
                    WinOverlayer(3);
                    break;
            case 3: Completed[3]=4;
                    WinOverlayer(3);
                    break;
        }
        if(TitleManager.AIblue){
            for(int i=0; i<4;i++){
                BluePucks[i].GetComponent<AI>().enabled=true;
            }
        }
        if(TitleManager.AIyellow){
            for(int i=0; i<4;i++){
                YellowPucks[i].GetComponent<AI>().enabled=true;
            }
        }
        if(TitleManager.AIgreen){
            for(int i=0; i<4;i++){
                GreenPucks[i].GetComponent<AI>().enabled=true;
            }
        }
    }
    private void Update() {
        if(Input.GetKey(KeyCode.Escape)){
            back();
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
    public void back(){
        if(screen==0){
        Menu.SetActive(true);
        screen=1;
        }
        else
        SceneManager.LoadScene("TitleScreen");
    }
    public void Resume(){
        screen=0;
        Menu.SetActive(false);
    }
}