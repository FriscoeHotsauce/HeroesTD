using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ability
{

  void activateAbility();

  bool isAvailable();

  float timeUntilReady();
}

public enum AbilityStatus
{
  Active, Cooldown, Ready
}
