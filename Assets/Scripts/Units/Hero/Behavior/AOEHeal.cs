using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEHeal : MonoBehaviour, PassiveBehavior
{
  public float tickRate;
  public Utils.UnitAlignment targetAlignment;
  public float range;
  public int overheal;

  private Stats sourceStats;
  private float nextTickTime;
  void Start()
  {
    sourceStats = GetComponent<Stats>();
    nextTickTime = 0.0f;
  }

  public virtual bool shouldExecute()
  {
    return Time.time > nextTickTime;
  }

  public virtual void executeBehavior()
  {
    List<GameObject> targetsInRange = Utils.getTaggedObjectsInRange(transform.position, range, targetAlignment.ToString());
    targetsInRange.ForEach(target =>
    {
      if (!target.GetComponent<HealthBar>().isMaxHealth(overheal))
      {
        target.GetComponent<HealthBar>().heal(sourceStats.getMagic(), overheal);
      }
    });
    nextTickTime = Time.time + tickRate;
  }
}
