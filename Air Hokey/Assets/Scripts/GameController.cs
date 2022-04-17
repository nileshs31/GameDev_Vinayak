using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject FinishUI;
    public GameObject PauseUI,SoundUI;
    
    void Start(){
        if(SceneScript.Difficulty>0){//Multiplayer Script Change
        GameObject.Find("Player2").GetComponent<PlayerMovement>().enabled=false;
        GameObject.Find("Player2").GetComponent<AI>().enabled=true;
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale=1;
    }

    public void Finish(string s){
        FinishUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text=s;
        FinishUI.SetActive(true);
        //GameObject.Find("Player").SetActive(false);
        //GameObject.Find("Player2").SetActive(false);
    }
    public void Pause(){
        Time.timeScale=0;
        PauseUI.SetActive(true);
    }
    public void Resume(){
        Time.timeScale=1;
        PauseUI.SetActive(false);
    }
    public void Sound(){
        if(SceneScript.Sounds){
            SoundUI.GetComponentInChildren<Text>().text="Sounds : Off";
            SceneScript.Sounds=false;
        }
        else{
            SoundUI.GetComponentInChildren<Text>().text="Sounds : On";
            SceneScript.Sounds=true;
        }
    }
}
