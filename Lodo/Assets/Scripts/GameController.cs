using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] SafePointes,WinOverlay,BluePucks,YellowPucks,GreenPucks;
    public GameObject Menu;
    public int[] AllIN,Completed,CircleOn;
    public Pucks[] AllPucks;
    int screen=0,pos=0;
    List<string>[] InCircle;
    List<int> VerticalCircles;
    IDictionary<string,int> PuckRef;
    void Start()
    {
        PuckRef=new Dictionary<string,int>(){
            {"Red1",0},
            {"Red2",1},
            {"Red3",2},
            {"Red4",3},
            {"Blue1",4},
            {"Blue2",5},
            {"Blue3",6},
            {"Blue4",7},
            {"Green1",8},
            {"Green2",9},
            {"Green3",10},
            {"Green4",11},
            {"Yellow1",12},
            {"Yellow2",13},
            {"Yellow3",14},
            {"Yellow4",15},
        };
        VerticalCircles=new List<int>(){2,3,6,7};
        InCircle=new List<string>[8];
        for(int i=0;i<8;i++){
            InCircle[i]=new List<string>();
        }
        AllIN = new int[4];
        Completed = new int[4];
        CircleOn = new int[8];
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
        pos=1;
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
        Pucks.OverlapFixed += SafePointFix;
        DiceRoll.Rolled += SafePointRefix;
    }
    private void Update() {
        if(Input.GetKey(KeyCode.Escape)){
            back();
        }
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
    public void WinOverlayer(int p){
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
    public void CircleHold(int CircleNumber, string objectname){
        InCircle[CircleNumber].Add(objectname);
        CircleOn[CircleNumber]+=1;
        Adjuster(CircleNumber);
    }
    public void CircleLeave(int CircleNumber, string objectname){
        InCircle[CircleNumber].Remove(objectname);
        CircleOn[CircleNumber]-=1;
        Adjuster(CircleNumber);
    }
    void Adjuster(int index){
        switch(CircleOn[index]){
            case 1:{
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position;
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.localScale=new Vector3(0.49f,0.49f,0.49f);
                    break;
            }
            case 2:{
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.localScale=new Vector3(0.25f,0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.transform.localScale=new Vector3(0.25f,0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.GetComponent<BoxCollider>().size=new Vector3(1.5f,1.5f,1.5f);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.GetComponent<BoxCollider>().size=new Vector3(1.5f,1.5f,1.5f);
                if(VerticalCircles.Contains(index)){
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0,0.25f);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position - new Vector2(0,0.25f);
                    break;
                    }
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0.25f,0);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position - new Vector2(0.25f,0);
                    break;
            }
            case 3:{
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.localScale=new Vector3(0.25f,0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.transform.localScale=new Vector3(0.25f,0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][2]]].gameObject.transform.localScale=new Vector3(0.25f,0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.GetComponent<BoxCollider>().size=new Vector3(1.5f,1.5f,1.5f);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.GetComponent<BoxCollider>().size=new Vector3(1.5f,1.5f,1.5f);
                    AllPucks[PuckRef[InCircle[index][2]]].gameObject.GetComponent<BoxCollider>().size=new Vector3(1.5f,1.5f,1.5f);
                if(VerticalCircles.Contains(index)){
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0,0.25f);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position - new Vector2(0,0.25f);
                    break;
                    }
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position - new Vector2(0.25f,0);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0.25f,0);
                    break;
            }
            case 4:{
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.localScale=new Vector3(0.25f,0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.transform.localScale=new Vector3(0.25f,0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][2]]].gameObject.transform.localScale=new Vector3(0.25f,0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][3]]].gameObject.transform.localScale=new Vector3(0.25f,0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(-0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0.25f,-0.25f);
                    AllPucks[PuckRef[InCircle[index][2]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(-0.25f,0.25f);
                    AllPucks[PuckRef[InCircle[index][3]]].gameObject.transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0.25f,-0.25f);
                    AllPucks[PuckRef[InCircle[index][0]]].gameObject.GetComponent<BoxCollider>().size=new Vector3(1.5f,1.5f,1.5f);
                    AllPucks[PuckRef[InCircle[index][1]]].gameObject.GetComponent<BoxCollider>().size=new Vector3(1.5f,1.5f,1.5f);
                    AllPucks[PuckRef[InCircle[index][2]]].gameObject.GetComponent<BoxCollider>().size=new Vector3(1.5f,1.5f,1.5f);
                    AllPucks[PuckRef[InCircle[index][3]]].gameObject.GetComponent<BoxCollider>().size=new Vector3(1.5f,1.5f,1.5f);
                    break;
            }
        }
    }
}