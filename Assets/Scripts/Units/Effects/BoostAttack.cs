using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAttack : Effect
{
  private int boostAmmount;
  private EffectState currentState;
  private float effectEnd;
  private Stats targetStats;

  public BoostAttack(float effectDuration, int boostAmmount, Stats targetStats)
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
    targetStats.addAttack(boostAmmount);
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
    targetStats.subtractAttack(boostAmmount);
  }
}
