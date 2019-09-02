using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    use to begin the wave
 */
public class Begin : MonoBehaviour
{
    public WaveManager waveManager;

    void Start(){
        waveManager = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
    }

    public void begin(){
        waveManager.begin();
        Destroy(gameObject, 0);
    }
}
