using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//must be placed on a class with Stats
public class BehaviourExecutor : MonoBehaviour
{
  public Utils.UnitType unitType;
  private Stats stats;
  private float attackSpeed;
  private float nextAttackTime;
  private List<Behavior> behaviors;

  void Start()
  {
    stats = GetComponent<Stats>();
    attackSpeed = Utils.calculateAttackRate(unitType, stats.getAgility());
    behaviors = GetComponents<Behavior>().ToList();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (Time.time > nextAttackTime)
    {
      executeBehaviors();
      nextAttackTime = Time.time + attackSpeed;
    }
  }

  private void executeBehaviors()
  {
    foreach (Behavior behavior in behaviors)
    {
      if (behavior.shouldExecute())
      {
        behavior.executeBehavior();
        //making an assumption right now that only one behaviour should execute, in the future I'd like to add more robust execution criteria
        break;
      }
    }
  }
}
