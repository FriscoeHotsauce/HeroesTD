using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//must be placed on a class with Stats
public class BehaviourExecutor : MonoBehaviour
{

  public List<Effect> effects;
  public List<Behavior> behaviors;

  void Start()
  {
    effects = new List<Effect>();
    behaviors = GetComponents<Behavior>().ToList();
  }

  void FixedUpdate()
  {
    executeBehaviors();
    updateAndExecuteEffects();
  }

  public void executeBehaviors()
  {
    bool actionsExceeded = false;
    foreach (Behavior behavior in behaviors)
    {
      if (behavior.shouldExecute())
      {
        if (behavior is TimedBehavior)
        {
          if (!actionsExceeded)
          {
            behavior.executeBehavior();
            actionsExceeded = true;
          }
          else
          {
            (behavior as TimedBehavior).resetCooldown();
          }
        }

        if (behavior is PassiveBehavior)
        {
          behavior.executeBehavior();
        }
      }
    }
  }

  public void addEffect(Effect effect)
  {
    effect.beginEffect();
    effects.Add(effect);
  }

  public void updateAndExecuteEffects()
  {
    //use a separate list for removal because it's bad manners to modify the one you're iterating over
    List<Effect> effectsToRemove = new List<Effect>();

    foreach (Effect effect in effects)
    {
      //always update the cooldown first. Update cooldown may change the state of the effect
      effect.updateCooldown();

      if (effect.getEffectState() == EffectState.Activate)
      {
        effect.activateEffect();
      }
      else if (effect.getEffectState() == EffectState.EndEffect)
      {
        effect.endEffect();
        effectsToRemove.Add(effect);
      }
    }

    foreach (Effect effect in effectsToRemove)
    {
      effects.Remove(effect);
    }
  }


}
