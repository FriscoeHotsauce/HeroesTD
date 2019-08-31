using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
   
    public string waveDisplayPrefix ="Wave: ";
    public string waveCooldownPrefix = "Next wave in: ";
    public Image waveCooldownRadialDisplay;
    public Text waveDisplay;
    public Text waveCooldown;
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
        waveLexicon = GetComponents<WaveBuilder>().ToList();
        totalWaves = getMaxWave();
        setWaveDisplayText();
        waveCooldownRadialDisplay.gameObject.SetActive(false);
    }

    void Update(){
        setWaveCountdown();
    }

    void FixedUpdate(){
        if(!isWaveEnded()){
            advanceTimeAndSpawnEnemies();
        } else if(!waveDelaySet) {
            nextWaveStart = Time.time + waveDelay;
            waveDelaySet = true;
            waveCooldownRadialDisplay.gameObject.SetActive(true);
        } else if(Time.time > nextWaveStart) {
            advanceToNextWave();
            waveDelaySet = false;
            waveCooldownRadialDisplay.gameObject.SetActive(false);
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
        setWaveDisplayText();
    }

    public int getCurrentWave(){
        return currentWave;
    }

    private void setWaveDisplayText(){
        waveDisplay.text = waveDisplayPrefix + currentWave + " / " + totalWaves;
    }

    private void setWaveCountdown(){
        if(waveDelaySet){
            waveCooldown.text = waveCooldownPrefix + (String.Format("{0:.00}",Math.Round(nextWaveStart - Time.time, 2))); //this is stupid but looks so cool
            waveCooldownRadialDisplay.fillAmount = 1 - ((nextWaveStart-Time.time) / nextWaveStart);
        } else {
            waveCooldown.text = "";
        }
    }

    private int getMaxWave(){
        return waveLexicon.Max(x => x.getWaveIndex());
    }

}
