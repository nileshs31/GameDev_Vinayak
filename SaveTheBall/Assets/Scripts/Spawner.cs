using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Obstacle;
    private GameObject spawned;
    public static int score=0;
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }
    IEnumerator SpawnObstacle(){
        while(true){
        yield return new WaitForSeconds(Random.Range(1,2));
        score+=1;
        spawned=Instantiate(Obstacle);
            spawned.transform.position+=new Vector3(Random.Range(0,4),0,0);
            spawned.GetComponent<ObstacleScript>().speed=-Random.Range(20,40);
    }
}
}
