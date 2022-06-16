using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamepaly : MonoBehaviour
{
    public GameObject[] Bunny;
    public static int time=120;
    public static int score=0;
    public GameObject GameOver,MainGame;
    public GameObject Score,finalscore,Time;
    //public static Vector2 Basket;
    void Start()
    {
        //Basket=GameObject.Find("MainBasket").GetComponent<Transform>().position;
        StartCoroutine("TimeReducer");
        StartCoroutine("BunnySpanner");
    }
    void Update()
    {
        Score.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
        if(time<1){
            finalscore.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
            MainGame.SetActive(false);
            GameOver.SetActive(true);
        }

    }
    private IEnumerator TimeReducer(){
        while(time>0){
        Time.GetComponent<TMPro.TextMeshProUGUI>().text = time.ToString();
        yield return new WaitForSeconds(1);
        time-=1;
        }
    }
    private IEnumerator BunnySpanner(){
        while(time>0){
            Instantiate(Bunny[Random.Range(0,3)]);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
