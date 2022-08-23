using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject BackButton,OnlineButton,OfflineButton,player2,player3,player4,Quitbutton,PlayButton;
    public GameObject Player4UI,Player3UI,Player2UI;
    public static int playercount;
    public static bool AIblue,AIgreen,AIyellow;
    int screen=0;

    private void OnEnable() {
        DontDestroyOnLoad(gameObject);
        playercount=4;
        AIblue=false;
        AIyellow=false;
        AIgreen=false;
    }
    public void Offline(){
        BackButton.SetActive(true);
        player2.SetActive(true);
        player3.SetActive(true);
        player4.SetActive(true);
        Quitbutton.SetActive(false);
        OnlineButton.SetActive(false);
        OfflineButton.SetActive(false);
    }
    public void Quit(){
        Application.Quit();
    }
    public void Back(){
        screen=1;
        BackButton.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        Quitbutton.SetActive(true);
        OnlineButton.SetActive(true);
        OfflineButton.SetActive(true);
    }
    public void P2(){
        Player4UI.SetActive(true);
        Player2UI.SetActive(false);
        BackButton.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        PlayButton.SetActive(true);
        BackButton.SetActive(false); 
        playercount=2;
        screen = 2;
    }
    public void P3(){
        Player4UI.SetActive(true);
        Player3UI.SetActive(false);
        BackButton.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        PlayButton.SetActive(true);
        BackButton.SetActive(false);  
        playercount=3;
        screen = 2;
    }
    public void P4(){
        Player4UI.SetActive(true);
        BackButton.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        PlayButton.SetActive(true);
        BackButton.SetActive(false); 
        screen = 2;
    }
    private void Update() {
        if(Input.GetKey(KeyCode.Escape)){
            switch(screen){
                case 0: Quit();
                break;
                case 1: Back();
                        screen=0;
                break;
                case 2:
                        BacktoMultiplayer();
                break;
            }
        }
    }
    public void AIYellowToggle(bool ai){
        AIyellow=ai;
    }
    public void AIBlueToggle(bool ai){
        AIblue=ai;
    }
    public void AIGreenToggle(bool ai){
        AIgreen=ai;
    }
    public void PlayGame(){
        SceneManager.LoadScene("Gameplay");
    }
    public void BacktoMultiplayer(){
        BackButton.SetActive(true);
        player2.SetActive(true);
        player3.SetActive(true);
        player4.SetActive(true);
        Player3UI.SetActive(true);
        Player2UI.SetActive(true);
        Player4UI.SetActive(false);
        PlayButton.SetActive(false);
    }
}
