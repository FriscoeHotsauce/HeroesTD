using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

  public string waveDisplayPrefix = "Wave: ";
  public string waveCooldownPrefix = "Next wave in: ";
  public Image waveCooldownRadialDisplay;
  public Text waveDisplay;
  public Text waveCooldown;
  public List<WaveBuilder> waveLexicon;
  public int currentWave;
  public int totalWaves;
  public float nextWaveStart;
  public float waveDelay;
  public GameState currentGameState;

  //a boolean to tell us if we should be waiting for the next wave to start
  private bool waveDelaySet;

  public enum GameState { Preparation, WavesInProgress, WavesConcluded, EnemiesCleared }

  void Start()
  {
    currentWave = 1;
    waveDelaySet = false;
    currentGameState = GameState.Preparation;
    waveLexicon = GetComponentsInChildren<WaveBuilder>().ToList();
    totalWaves = getMaxWave();
    setWaveDisplayText();
    waveCooldownRadialDisplay.gameObject.SetActive(false);
  }

  void Update()
  {
    setWaveCountdown();
  }

  void FixedUpdate()
  {
    if (currentGameState == GameState.WavesInProgress || currentGameState == GameState.WavesConcluded)
    {
      if (!isWaveEnded())
      {
        advanceTimeAndSpawnEnemies();
      }
      else if (currentWave == totalWaves && isWaveEnded() && currentGameState != GameState.EnemiesCleared)
      {
        endGame();
      }
      else if (!waveDelaySet && currentGameState == GameState.WavesInProgress)
      {
        nextWaveStart = Time.time + waveDelay;
        waveDelaySet = true;
        waveCooldownRadialDisplay.gameObject.SetActive(true);
      }
      else if (Time.time > nextWaveStart)
      {
        advanceToNextWave();
        waveDelaySet = false;
        waveCooldownRadialDisplay.gameObject.SetActive(false);
      }

    }
  }

  public void begin()
  {
    currentGameState = GameState.WavesInProgress;
    setInitialWaveDelays();
  }

  public void advanceTimeAndSpawnEnemies()
  {
    List<WaveBuilder> currentWaveBuidlers = waveLexicon.Where(wave => wave.getWaveIndex() == currentWave).ToList();
    foreach (WaveBuilder waveSegment in currentWaveBuidlers)
    {
      if (Time.time > waveSegment.getNextSpawnTime())
      {
        waveSegment.spawnEnemy();
      }
    }
  }

  public bool isWaveEnded()
  {
    //how bout them lambdas
    return (waveLexicon.Where(wave => wave.getWaveIndex() == currentWave)
        .Where(wave => wave.getUnitsSpawned() < wave.getUnitsToSpawn()).ToList().Count == 0);
  }


  public void advanceToNextWave()
  {
    currentWave = currentWave + 1;
    setWaveDisplayText();
    setInitialWaveDelays();
  }

  public void setInitialWaveDelays()
  {
    waveLexicon.Where(wave => wave.getWaveIndex() == currentWave).ToList().ForEach(wave => wave.setInitialDelay());
  }

  public int getCurrentWave()
  {
    return currentWave;
  }

  public GameState getGameState()
  {
    return currentGameState;
  }

  private void setWaveDisplayText()
  {
    waveDisplay.text = waveDisplayPrefix + currentWave + " / " + totalWaves;
  }

  private void setWaveCountdown()
  {
    if (waveDelaySet)
    {
      waveCooldown.text = waveCooldownPrefix + (String.Format("{0:.00}", Math.Round(nextWaveStart - Time.time, 2))); //this is stupid but looks so cool
      waveCooldownRadialDisplay.fillAmount = ((nextWaveStart - Time.time) / waveDelay);
    }
    else
    {
      waveCooldown.text = "";
    }
  }

  private int getMaxWave()
  {
    return waveLexicon.Max(x => x.getWaveIndex());
  }

  private void endGame()
  {
    currentGameState = GameState.WavesConcluded;
    waveDelaySet = false;
    if (!Utils.enemiesExist())
    {
      currentGameState = GameState.EnemiesCleared;
      Debug.Log("Game Over!");
    }
  }

}
