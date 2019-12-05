using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Effect
{

  void beginEffect();

  void updateCooldown();

  void activateEffect();

  void endEffect();
}
