using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgilityBoost : MonoBehaviour, Ability
{

  public float duration;
  public float cooldownTime;
  public float cooldownEnd;
  [Tooltip("Should be a float value i.e. .20 is a 20% boost")]
  public float percentBoost;
  public HeroStats heroStats;
  public AbilityStatus currentStatus;
  public BehaviourExecutor heroBehaviorExecutor;

  private int boostAmmount; //the bonus added, and the amount that should be subtracted when the boost ends

  void Start()
  {
    heroStats = GetComponent<HeroStats>();
    currentStatus = AbilityStatus.Cooldown;
    cooldownEnd = Time.time + cooldownTime;
    heroBehaviorExecutor = GetComponent<BehaviourExecutor>();
  }
  public void activateAbility()
  {
    //round up because we are good guy
    boostAmmount = (int)Math.Ceiling(heroStats.getAgility() * percentBoost);
    //add an effect
    heroBehaviorExecutor.addEffect(new BoostAgility(duration, boostAmmount, heroStats));
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
