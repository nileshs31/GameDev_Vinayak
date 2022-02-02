using UnityEngine.SceneManagement;
using UnityEngine;

public class Restrart : MonoBehaviour
{
    public void play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
