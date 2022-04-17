using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneScript : MonoBehaviour
{
    public static int Difficulty=0;
    public static bool Sounds=true;
    public GameObject UI;
    public void ToggleEasy(){
        Difficulty=1;
    }
    public void ToggleMedium(){
        Difficulty=2;
    }
    public void ToggleHard(){
        Difficulty=3;
    }
        public void ToggleMP(){
        Difficulty=0;
    }

    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }
    public void StartGame(){
        SceneManager.LoadScene("Gameplay");
    }
    public void OnlineGame(){
        Difficulty=-1;
        SceneManager.LoadScene("Lobby");
    }
    public void Sound(){
        if(Sounds){
            UI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text="Sounds : Off";
            Sounds=false;
        }
        else{
            UI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text="Sounds : On";
            Sounds=true;
        }
    }
    public void Exit(){
        Application.Quit();
    }    
}