using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
    @param startingRequisitionPoints - the number of requisition points allotted before waves begin
    @param currentRequisitionPoints - the current number of requsition points
    @param requsitionPointsPerTick - How many points to allot between the delay
    @param requisitionPointDelay - the time delay between requistion points
    @param requisitionTextPrefix - The text (if any) to display before current requisition points
    @param gameMaster - The object that contains the WaveManager
    @param requisitionDisplayText - The Text UI Element to display our requisition counter
 */
public class RequisitionManager : MonoBehaviour
{

  public int startingRequisitionPoints;
  public int currentRequisitionPoints;
  public int requsitionPointsPerTick;
  public float requisitionPointDelay;
  public string requisitionTextPrefix = "REQ: ";
  public Text requisitionDisplayText;
  public Image requisitionCooldownDisplay;

  public float nextRequisitionPoint;
  private WaveManager waveManager;
  private HeroSelector heroSelector;

  void Start()
  {
    waveManager = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
    currentRequisitionPoints = startingRequisitionPoints;
    setRequisitionPointsView();
    nextRequisitionPoint = 0.0f;
    heroSelector = GameObject.FindWithTag("HeroSelector").GetComponent<HeroSelector>();
  }

  void Update()
  {
    requisitionCooldownDisplay.fillAmount = (nextRequisitionPoint - Time.time) / requisitionPointDelay;
  }

  void FixedUpdate()
  {
    //Debug.Log("Waves Started "+ waveManager.getWavesStarted());
    if (waveManager.getGameState() == WaveManager.GameState.WavesInProgress
    || waveManager.getGameState() == WaveManager.GameState.WavesConcluded)
    {
      if (Time.time > nextRequisitionPoint)
      {
        applyPointsAndUpdateTime();
      }
    }
  }

  public void deductPoints(int pointsToDeduct)
  {
    currentRequisitionPoints = currentRequisitionPoints - pointsToDeduct;
    setRequisitionPointsView();
    heroSelector.updateButtonValidity();
  }

  public int getCurrentRequisitionPoints()
  {
    return currentRequisitionPoints;
  }

  public void addPoints(int points)
  {
    currentRequisitionPoints += points;
    setRequisitionPointsView();
  }

  private void applyPointsAndUpdateTime()
  {
    currentRequisitionPoints = currentRequisitionPoints + requsitionPointsPerTick;
    nextRequisitionPoint = Time.time + requisitionPointDelay;
    setRequisitionPointsView();
    heroSelector.updateButtonValidity();
  }

  private void setRequisitionPointsView()
  {
    requisitionDisplayText.text = requisitionTextPrefix + currentRequisitionPoints;
  }
}
