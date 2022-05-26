using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameController1 : MonoBehaviour
{
    public GameObject FinishUI;
    public GameObject PauseUI,SoundUI;
    public bool menu = false;
    //PhotonView PV;
    
    public void PlayAgain()
    {
        if(PhotonNetwork.IsMasterClient)
        GameObject.Find("Ball").GetComponent<BallMovement3>().gcPlayAgain1();
        else
        GameObject.Find("Ball").GetComponent<BallMovement3>().gcPlayAgain2();
        Finish("Waiting for opponent");
    }
    public void MainMenu()
    {
        //Time.timeScale=1;
        BallMovement3.Play2=false;
        BallMovement3.Play1=false;
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
        menu=true;
        //Time.timeScale=0;
        PauseUI.SetActive(true);
    }
    public void Resume(){
        //Time.timeScale=1;
        menu=false;
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
    private void Update() {
        if(Input.GetKey(KeyCode.Escape)){
            if(menu){
                PauseUI.SetActive(false);
                menu=true;
            }
            else{
                menu=false;
                PauseUI.SetActive(true);
            }
        }
    }
}
