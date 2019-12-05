using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequisitionBoost : MonoBehaviour, Ability
{
  public int requisitionToAdd;
  public AbilityStatus currentStatus;
  public float cooldownTime;
  public float cooldownEnd;
  public string description;
  public RequisitionManager requisitionManager;

  void Start()
  {
    requisitionManager = GameObject.FindGameObjectWithTag("RequisitionManager").GetComponent<RequisitionManager>();
    currentStatus = AbilityStatus.Cooldown;
    cooldownEnd = Time.time + cooldownTime;
  }

  public void activateAbility()
  {
    requisitionManager.addPoints(requisitionToAdd);
    currentStatus = AbilityStatus.Cooldown;
    cooldownEnd = Time.time + cooldownTime;
  }

  public bool isAvailable()
  {
    return currentStatus == AbilityStatus.Ready;
  }

  public float timeUntilReady()
  {
    return cooldownEnd - Time.time;
  }

  void Update()
  {
    if (currentStatus == AbilityStatus.Cooldown && Time.time > cooldownEnd)
    {
      currentStatus = AbilityStatus.Ready;
    }
  }
}
