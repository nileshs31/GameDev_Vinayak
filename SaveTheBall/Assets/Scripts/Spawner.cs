using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Obstacle;
    private GameObject spawned;
    public static int score=0;
    private float[] Locationx={-3f,-2f,-1f,0f,1f,2f,3f,4f,5f};
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }
    IEnumerator SpawnObstacle(){
        while(true){
        yield return new WaitForSeconds(Random.Range(0.2f,1));
        score+=1;
        if(score%10==0)
        ObstacleScript.speed-=2;
        spawned=Instantiate(Obstacle);
            spawned.transform.position+=new Vector3(Locationx[(int)Random.Range(0,8)],0,0);
            //spawned.GetComponent<ObstacleScript>().speed=-Random.Range(10,40);
    }
}
}
