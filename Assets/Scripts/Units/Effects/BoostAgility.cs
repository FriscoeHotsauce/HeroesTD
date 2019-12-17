using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class does NOT extend monobehavior, and shouldn't; use the Behavior Executor to drive updates.
public class BoostAgility : Effect
{
  private float boostAmmount;
  private EffectState currentState;
  private float effectEnd;
  private Stats targetStats;

  public BoostAgility(float effectDuration, float boostAmmount, Stats targetStats)
  {
    this.boostAmmount = boostAmmount;
    this.currentState = EffectState.Activate;
    this.effectEnd = Time.time + effectDuration;
    this.targetStats = targetStats;
  }

  public void beginEffect()
  {
    activateEffect();
  }

  public void updateCooldown()
  {
    if (Time.time > effectEnd)
    {
      currentState = EffectState.EndEffect;
    }
  }


  public void activateEffect()
  {
    targetStats.addAgility(boostAmmount);
    currentState = EffectState.OnCooldown;
  }

  public EffectState getEffectState()
  {
    return currentState;
  }

  public EffectType getEffectType()
  {
    return EffectType.Buff;
  }

  public void endEffect()
  {
    targetStats.subtractAgility(boostAmmount);
  }

}
