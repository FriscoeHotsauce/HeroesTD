using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Effect
{

  void beginEffect();

  void updateCooldown();

  void activateEffect();

  EffectState getEffectState();

  EffectType getEffectType();

  void endEffect();
}

public enum EffectState
{
  OnCooldown, Activate, EndEffect
}

public enum EffectType
{
  Buff, Debuff
}