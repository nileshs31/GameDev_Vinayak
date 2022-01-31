using UnityEngine.UI;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void Pause(){
        if(Time.timeScale==1){
        Time.timeScale=0;
        gameObject.GetComponentInChildren<Text>().text="Resume";
        }
        else{
        Time.timeScale=1;
        gameObject.GetComponentInChildren<Text>().text="Pause";
        }
    }
}