using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject FinishUI;
    public GameObject PauseUI, SoundUI;
    public bool menu = false;

    void Start()
    {
        if (SceneScript.Difficulty > 0)
        {//Multiplayer Script Change
            GameObject.Find("Player2").GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("Player2").GetComponent<AI>().enabled = true;
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void Finish(string s)
    {
        Time.timeScale=0;
        menu=true;
        FinishUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = s;
        FinishUI.SetActive(true);
        //GameObject.Find("Player").SetActive(false);
        //GameObject.Find("Player2").SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
        menu = true;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }
    public void Sound()
    {
        if (SceneScript.Sounds)
        {
            SoundUI.GetComponentInChildren<Text>().text = "Sounds : Off";
            SceneScript.Sounds = false;
        }
        else
        {
            SoundUI.GetComponentInChildren<Text>().text = "Sounds : On";
            SceneScript.Sounds = true;
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (menu)
            {
                Time.timeScale = 1;
                PauseUI.SetActive(false);
                FinishUI.SetActive(false);
                menu = false;
            }
            else
            {
                Time.timeScale = 0;
                PauseUI.SetActive(true);
                menu = true;
            }
        }
    }
}
