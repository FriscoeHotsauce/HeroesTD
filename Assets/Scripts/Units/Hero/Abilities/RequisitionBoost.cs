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
  }

  public virtual void activateAbility()
  {
    requisitionManager.addPoints(requisitionToAdd);
    currentStatus = AbilityStatus.Cooldown;
    cooldownEnd = Time.time + cooldownTime;
  }

  public virtual bool isAvailable()
  {
    return currentStatus == AbilityStatus.Ready;
  }

  void Update()
  {
    if (currentStatus == AbilityStatus.Cooldown && cooldownEnd > Time.time)
    {
      currentStatus = AbilityStatus.Ready;
    }
  }
}
