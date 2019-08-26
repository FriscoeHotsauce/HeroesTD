using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
   
    public List<WaveBuilder> waveLexicon;
    //public List<Transform> spawnLocations;
    public int currentWave;
    public int totalWaves;
    public float nextWaveStart;
    public float waveDelay;
    //a boolean to tell us if we should be waiting for the next wave to start
    private bool waveDelaySet;

    void Start(){
        currentWave = 1;
        waveDelaySet = false;
    }

    void FixedUpdate(){
        if(!isWaveEnded()){
            advanceTimeAndSpawnEnemies();
        } else if(!waveDelaySet) {
            nextWaveStart = Time.time + waveDelay;
            waveDelaySet = true;
        } else if(Time.time > nextWaveStart) {
            advanceToNextWave();
            waveDelaySet = false;
        }
    }

    public void advanceTimeAndSpawnEnemies(){
        List<WaveBuilder> currentWaveBuidlers = waveLexicon.Where(wave => wave.getWaveIndex() == currentWave).ToList();
        foreach(WaveBuilder waveSegment in currentWaveBuidlers){
            if(Time.time > waveSegment.getNextSpawnTime()){
                waveSegment.spawnEnemy();
            }
        }
    }

    public bool isWaveEnded(){
        //how bout them lambdas
        return waveLexicon.Where(wave => wave.getWaveIndex() == currentWave)
            .Where(wave => wave.getUnitsSpawned() < wave.getUnitsToSpawn()).ToList().Count == 0;
    }

    public void advanceToNextWave(){
        currentWave = currentWave + 1;
    }

    public int getCurrentWave(){
        return currentWave;
    }

}
