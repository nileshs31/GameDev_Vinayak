using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public float Speed=3;
    Rigidbody rb;
    public GameObject Table,NamePanel,WinPanel,Bar;
    public GameObject[] Coins;
    public TextMeshProUGUI newName,Oldname,WinText,Woncoin;
    TextMeshProUGUI Wincoin;
    bool move=false,button=false;
    void Start() {
        rb=Table.GetComponent<Rigidbody>();
    }
    private void Update() {
        if(move)
        rb.angularVelocity=Vector3.forward*Speed;
    }
    public void OKbutton(){
        WinPanel.SetActive(false);
        button=true;
    }
    public void SpinButton(){
        if(!button)
        return;
        StartCoroutine("MovingSpeed");
        move=true;
        button=false;
    }
    public void PlayBotton(){
        newName.text=Oldname.text;
        NamePanel.SetActive(false);
        button=true;
    }
    public void QuitButton(){
        Application.Quit();
    }
    IEnumerator MovingSpeed(){
        while(true){
        yield return new WaitForSeconds(0.3f);
        Speed+=1f;
        if(Speed>10)
            StartCoroutine("EndRotate");
        }
    }
    IEnumerator EndRotate(){
        StopCoroutine("MovingSpeed");
        while(true){
        yield return new WaitForSeconds(Random.Range(0.5f,0.8f));
        Speed-=1;
        if(Speed<2){
        StopAllCoroutines();
        move=false;
        StartCoroutine("DisplayCoinsWin");
        }
        }
    }
    IEnumerator DisplayCoinsWin(){
        yield return new WaitForSeconds(5);
        float One=0;
        int i=0;
        foreach(GameObject coin in Coins){
            i+=1;
            if(coin.transform.position.y>One){
            One=coin.transform.position.y;            
            Wincoin=coin.GetComponent<TextMeshProUGUI>();
            }
        }
        WinText.text="You Have Won " + Wincoin.text + " coins";
        WinPanel.SetActive(true);
        Woncoin.text=(int.Parse(Woncoin.text) + int.Parse(Wincoin.text)).ToString();
        
    }
}