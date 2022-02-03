using UnityEngine.SceneManagement;
using UnityEngine;

public class StrartGame : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("Level");
    }
}
