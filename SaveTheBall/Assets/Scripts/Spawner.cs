using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Obstacle;
    private GameObject spawned;
    public static int score=0;
    private float[] Locationx={1.25f,0f,2.55f};
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }
    IEnumerator SpawnObstacle(){
        while(true){
        yield return new WaitForSeconds(Random.Range(1,2));
        score+=1;
        spawned=Instantiate(Obstacle);
            spawned.transform.position+=new Vector3(Locationx[(int)Random.Range(0,3)],0,0);
            //spawned.GetComponent<ObstacleScript>().speed=-Random.Range(10,40);
    }
}
}
