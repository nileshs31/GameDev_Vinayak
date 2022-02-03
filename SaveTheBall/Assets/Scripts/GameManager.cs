using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject PauseText;
    public GameObject Score;


    public void Pause(){
        if(Time.timeScale==1){
        Time.timeScale=0;
        PauseText.GetComponent<Text>().text="Resume";
        }
        else{
        Time.timeScale=1;
        PauseText.GetComponent<Text>().text="Pause";
        }
    } 
    public void play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        sc=0;
        Time.timeScale=1;
    }   
    public static int sc=0;
    void Update()
    {
        Score.GetComponent<Text>().text=sc.ToString("0");
    }    
}

