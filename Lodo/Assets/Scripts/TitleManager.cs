using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject BackButton,OnlineButton,OfflineButton,player2,player3,player4,Quitbutton;
    public static int playercount=4;

    private void OnEnable() {
        DontDestroyOnLoad(gameObject);
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
        BackButton.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        Quitbutton.SetActive(true);
        OnlineButton.SetActive(true);
        OfflineButton.SetActive(true);
    }
    public void P2(){
        playercount=2;
        SceneManager.LoadScene("Gameplay");
    }
    public void P3(){
        playercount=3;
        SceneManager.LoadScene("Gameplay");
    }
    public void P4(){
        SceneManager.LoadScene("Gameplay");
    }
}
