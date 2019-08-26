using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
    Wave builder centralizes wave construction. Wave Manger handles timing and firing spawn events.
 */
[System.Serializable]
public class WaveBuilder : MonoBehaviour
{
    public int waveIndex;
    public float spawnInterval;
    public Transform spawnLocation;
    public Transform spawnPrefab;
    public int unitsToSpawn;
    private int unitsSpawned;
    private float nextSpawnTime;

    public WaveBuilder(
        int waveIndex,
        float spawnInterval,
        Transform spawnLocation,
        Transform spawnPrefab,
        int unitsToSpawn
    ) {
        this.waveIndex = waveIndex;
        this.spawnInterval = spawnInterval;
        this.spawnLocation = spawnLocation;
        this.spawnPrefab = spawnPrefab;
        this.unitsToSpawn = unitsToSpawn;
        unitsSpawned = 0;
        nextSpawnTime = 0.0f;
    }

    public int getWaveIndex(){
        return waveIndex;
    }

    public float getSpawnInterval(){
        return spawnInterval;
    }
    
    public float getNextSpawnTime(){
        return nextSpawnTime;
    }

    public int getUnitsToSpawn(){
        return unitsToSpawn;
    }

    public int getUnitsSpawned(){
        return unitsSpawned;
    }

    public void spawnEnemy(){
        Transform created = Instantiate(spawnPrefab, spawnLocation.position, spawnLocation.rotation);
        created.name = "Wave "+waveIndex+" - "+spawnLocation.name+" - "+unitsSpawned;
        unitsSpawned += 1;
        nextSpawnTime = Time.time + spawnInterval;
    }
}
