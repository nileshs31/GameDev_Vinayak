using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static int Difficulty=0;
    public void ToggleEasy(){
        Difficulty=1;
        print(Difficulty);
    }
    public void ToggleMedium(){
        Difficulty=2;
        print(Difficulty);
    }
    public void ToggleHard(){
        Difficulty=3;
        print(Difficulty);
    }
        public void ToggleMP(){
        Difficulty=0;
        print(Difficulty);
    }

    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }

    public void StartGame(){
        SceneManager.LoadScene("Gameplay");
    }
}
