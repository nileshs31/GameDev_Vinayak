using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject FinishUI;
    public GameObject PauseUI;
    
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
        GameObject.Find("Player").SetActive(false);
        GameObject.Find("Player2").SetActive(false);
    }
    public void Pause(){
        Time.timeScale=0;
        PauseUI.SetActive(true);
    }
    public void Resume(){
        Time.timeScale=1;
        PauseUI.SetActive(false);
    }
}
