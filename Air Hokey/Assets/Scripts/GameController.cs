using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject FinishUI;
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Finish(string s){
        FinishUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text=s;
        FinishUI.SetActive(true);
        Destroy(GameObject.Find("Player").GetComponent<PlayerMovement>());
        Destroy(GameObject.Find("Player2").GetComponent<AI>());
    }
}
