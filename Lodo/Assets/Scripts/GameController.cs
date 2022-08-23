using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] PucksAll,SafePointes,WinOverlay,BluePucks,YellowPucks,GreenPucks;
    public GameObject Menu;
    public int[] AllIN,Completed,CountOnCircle,PucksOnUnSafe;  //CicleOn - count of pucks on circle;
    public Pucks[] AllPucks;
    public string[] Lefter,Righter,Middler; //For UnsafeCollision
    Vector3 Smallscale,LargeCollider;
    int screen=0,pos=0;
    List<string>[] PucksInCircle;    //List of Puck's name present in current cicle;
    List<int> VerticalCircles;  //List of Circles on Vertical Path
    IDictionary<string,int> PuckRef;    //Mapping of Puck'n name with their reference in ALLPucks
    void Start()
    {
        Lefter = new string[4];
        Middler = new string[4];
        Righter = new string[4];
        LargeCollider=new Vector3(1.5f,1.5f,1.5f);
        Smallscale=new Vector3(0.25f,0.25f,0.25f);
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
        PucksInCircle=new List<string>[8];
        for(int i=0;i<8;i++){
            PucksInCircle[i]=new List<string>();
        }
        AllIN = new int[4];
        Completed = new int[4];
        CountOnCircle = new int[8];
        PucksOnUnSafe = new int[4];
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
        // if(pos==0)
        // WinOverlay[p].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
        // else
        WinOverlay[p].GetComponentInChildren<TMPro.TextMeshProUGUI>().text+=pos.ToString();
        pos++;
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
    public void CircleHold(int CircleNumber, string objectname){    //Called when Puck Collides with Circle (OnCollisionEnter)
        PucksInCircle[CircleNumber].Add(objectname);
        if(CountOnCircle[CircleNumber]==0)
        CountOnCircle[CircleNumber]+=1;
        else{
        CountOnCircle[CircleNumber]+=1;
        PucksAll[PuckRef[objectname]].transform.localScale=Smallscale;
        PucksAll[PuckRef[objectname]].GetComponent<BoxCollider>().size=LargeCollider;
        Adjuster(CircleNumber);
        }
    }
    public void CircleLeave(int CircleNumber, string objectname){   //Called when Puck Leaves the Circle (Pucks.MovePlayer)
        PucksAll[PuckRef[objectname]].transform.localScale=new Vector3(0.49f,0.49f,0.49f);
        PucksAll[PuckRef[objectname]].GetComponent<BoxCollider>().size=new Vector3(0.85f,0.85f,0.85f);
        PucksInCircle[CircleNumber].Remove(objectname);
        CountOnCircle[CircleNumber]-=1;
        Adjuster(CircleNumber);
    }
    void Adjuster(int index){   //Adjusts the scale, position, and collider size of pucks 
        switch(CountOnCircle[index]){
            case 1:{    //Normal Puck size/pos
                    PucksAll[PuckRef[PucksInCircle[index][0]]].transform.position=(Vector2)SafePointes[index].transform.position;
                    PucksAll[PuckRef[PucksInCircle[index][0]]].transform.localScale=new Vector3(0.49f,0.49f,0.49f);
                    PucksAll[PuckRef[PucksInCircle[index][0]]].GetComponent<BoxCollider>().size=new Vector3(0.85f,0.85f,0.85f);
                    break;
            }
            case 2:{
                    PucksAll[PuckRef[PucksInCircle[index][0]]].transform.localScale=Smallscale;
                    PucksAll[PuckRef[PucksInCircle[index][0]]].GetComponent<BoxCollider>().size=LargeCollider;
                if(VerticalCircles.Contains(index)){
                    PucksAll[PuckRef[PucksInCircle[index][0]]].transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0,0.25f);
                    PucksAll[PuckRef[PucksInCircle[index][1]]].transform.position=(Vector2)SafePointes[index].transform.position - new Vector2(0,0.25f);
                }
                else{
                    PucksAll[PuckRef[PucksInCircle[index][0]]].transform.position=(Vector2)SafePointes[index].transform.position - new Vector2(0.25f,0);
                    PucksAll[PuckRef[PucksInCircle[index][1]]].transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0.25f,0);
                }
                break;
            }
            case 3:{    //2 pucks shift 1 stays original pos
                PucksAll[PuckRef[PucksInCircle[index][2]]].transform.position=(Vector2)SafePointes[index].transform.position;
                if(VerticalCircles.Contains(index)){
                    PucksAll[PuckRef[PucksInCircle[index][0]]].transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0,0.25f);
                    PucksAll[PuckRef[PucksInCircle[index][1]]].transform.position=(Vector2)SafePointes[index].transform.position - new Vector2(0,0.25f);
                }
                else{
                    PucksAll[PuckRef[PucksInCircle[index][0]]].transform.position=(Vector2)SafePointes[index].transform.position - new Vector2(0.25f,0);
                    PucksAll[PuckRef[PucksInCircle[index][1]]].transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0.25f,0);
                }
                break;
            }
            case 4:{
                    PucksAll[PuckRef[PucksInCircle[index][0]]].transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0.25f,0.25f);
                    PucksAll[PuckRef[PucksInCircle[index][1]]].transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(0.25f,-0.25f);
                    PucksAll[PuckRef[PucksInCircle[index][2]]].transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(-0.25f,-0.25f);
                    PucksAll[PuckRef[PucksInCircle[index][3]]].transform.position=(Vector2)SafePointes[index].transform.position + new Vector2(-0.25f,0.25f);
                    break;
            }
        }
    }
}