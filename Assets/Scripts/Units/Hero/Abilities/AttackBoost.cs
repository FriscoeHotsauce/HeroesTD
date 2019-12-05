using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoost : MonoBehaviour, Ability
{
  public float duration;
  public float cooldownTime;
  public float cooldownEnd;
  [Tooltip("Should be a float value i.e. .20 is a 20% boost")]
  public float percentBoost;
  public HeroStats heroStats;
  public AbilityStatus currentStatus;

  private int boostAmmount; //the bonus added, and the amount that should be subtracted when the boost ends

  void Start()
  {
    heroStats = GetComponent<HeroStats>();
    currentStatus = AbilityStatus.Cooldown;
    cooldownEnd = Time.time + cooldownTime;
  }

  public void activateAbility()
  {
    //round up because we are good guy
    boostAmmount = (int)Math.Ceiling(heroStats.getAttack() * percentBoost);
    //add an effect
  }

  public bool isAvailable()
  {
    return currentStatus == AbilityStatus.Ready;
  }

  public float timeUntilReady()
  {
    return cooldownEnd - Time.time;
  }

  // Update is called once per frame
  void Update()
  {

  }
}
