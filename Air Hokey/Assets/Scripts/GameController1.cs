using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameController1 : MonoBehaviour
{
    public GameObject FinishUI;
    public GameObject PauseUI,SoundUI;
    public int play=0;
    //PhotonView PV;
    
    public void PlayAgain()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        play++;
        Finish("Waiting for opponent");
        if(play>1){
        GameObject.Find("Ball").GetComponent<BallMovement2>().ResetGame();
        FinishUI.SetActive(false);
        }
    }
    public void MainMenu()
    {
        //Time.timeScale=1;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
    }

    public void Finish(string s){
        FinishUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text=s;
        FinishUI.SetActive(true);
        // GameObject.Find("Player").SetActive(false);
        // GameObject.Find("Player2").SetActive(false);
    }
    public void Pause(){
        //Time.timeScale=0;
        PauseUI.SetActive(true);
    }
    public void Resume(){
        //Time.timeScale=1;
        PauseUI.SetActive(false);
    }
    // public void Exit(){
    //     Application.Quit();
    // }
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
