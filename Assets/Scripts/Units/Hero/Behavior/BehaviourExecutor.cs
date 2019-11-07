using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//must be placed on a class with Stats
public class BehaviourExecutor : MonoBehaviour
{
  private List<Behavior> behaviors;

  void Start()
  {
    behaviors = GetComponents<Behavior>().ToList();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    executeBehaviors();
  }

  private void executeBehaviors()
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
}
