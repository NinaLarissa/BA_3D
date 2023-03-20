using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Tutorial mainly by LittleByteGameStudio - https://www.youtube.com/watch?v=-k3RS6HQiy4


    public GameObject enemyToSpawn;
    public GameObject enemyToSpawn2;
    public Transform[] spawnPoints;
    private float repeatRate = 5.0f;

    // thru the invoke there will appear 4 enemies in total because of the delay, before the trigger will be destroyed


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            InvokeRepeating("SpawnEnemies", 0.5f, repeatRate);
            Destroy(gameObject, 10);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    //Enemies will spawn on trigger
    void SpawnEnemies()
    {
        foreach (var sp  in spawnPoints)
        {
            Instantiate(enemyToSpawn, sp.position, sp.rotation);
            Instantiate(enemyToSpawn2, sp.position, sp.rotation);
        }
    }


}
