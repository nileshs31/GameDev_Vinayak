using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //public bool dead=false;
    public GameObject Dead;
    public void GameOver(){
        //if(dead==false){
          //  dead=true;
            Dead.SetActive(true);
            //Cameras.Cam=false;
        }
    /*
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Cameras.Cam=true;
    }

    /*public void EndGame(){
        Invoke("Completed",1f);
    }
    void Completed(){
        Complete.SetActive(true);
        Cameras.Cam=false;
        Floor.SetActive(false);
        dead=true;
    }
    public void end(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Cameras.Cam=true;
    }*/
}

