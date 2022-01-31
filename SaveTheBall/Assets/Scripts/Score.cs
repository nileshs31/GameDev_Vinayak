using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int sc=0;
    void Update()
    {
        gameObject.GetComponent<Text>().text=sc.ToString("0");
    }
}
