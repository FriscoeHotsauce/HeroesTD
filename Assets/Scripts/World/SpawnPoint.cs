using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //# of units to spawn
    public int unitsToSpawn;
    //the time inbetween object instantiations
    public float spawnDelay;
    //unit to spawn
    public Transform prefab;
    //# of units spawned so far
    private int unitsSpawned;
    //next spawn time
    private float nextSpawnTime;

    void Start(){
        nextSpawnTime = 0.0f;
        unitsSpawned = 0;
    }

    void FixedUpdate(){
        if(Time.time > nextSpawnTime && unitsSpawned < unitsToSpawn){
            Transform created = Instantiate(prefab, transform.position, transform.rotation);
            created.name = transform.name + " - " + unitsSpawned;
            unitsSpawned = unitsSpawned + 1;
            nextSpawnTime = Time.time + spawnDelay;
        } else {
        //Destroy(gameObject);
        }
    }   
}
