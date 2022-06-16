using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public void startGame(){
        SceneManager.LoadScene("Game");
    }
    public void quitGame(){
        Application.Quit();
    }
    public void mainMenu(){
        Gamepaly.time=120;
        Gamepaly.score=0;
        SceneManager.LoadScene("Menu");
    }
}
