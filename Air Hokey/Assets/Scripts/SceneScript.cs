using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static int Difficulty=0;
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
}
