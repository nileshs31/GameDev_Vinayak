using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;

public class GameController1 : MonoBehaviour
{
    public GameObject FinishUI;
    public GameObject PauseUI;
    //PhotonView PV;
    
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
        // GameObject.Find("Player").SetActive(false);
        // GameObject.Find("Player2").SetActive(false);
    }
    // public void Pause(){
    //     Time.timeScale=0;
    //     PauseUI.SetActive(true);
    // }
    // public void Resume(){
    //     Time.timeScale=1;
    //     PauseUI.SetActive(false);
    // }
    // void Start(){
    //     if()
    //     {
    //         PV=GetComponent<PhotonView>();
    //         PhotonNetwork.Instantiate("Spwanner",Vector3.zero,Quaternion.identity);
    //     }
    // }
}
