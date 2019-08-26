using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int currentWave;
    public int totalWaves;
    
    void Start(){
        currentWave = 1;
    }

    public void advanceToNextWave(){
        currentWave = currentWave + 1;
    }

    public int getCurrentWave(){
        return currentWave;
    }
}
