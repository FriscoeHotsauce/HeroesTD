using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedAttack : MonoBehaviour, TimedBehavior
{
  public string animationName;
  public Transform missilePrefab;
  public Utils.UnitAlignment targetAlignment;

  private GameObject currentTarget;
  private Stats sourceStats;
  private Animator animator;
  public float range;
  private float attackSpeed;
  private float nextAttackTime;


  void Start()
  {
    animator = GetComponent<Animator>();
    sourceStats = GetComponent<Stats>();
    range = findRangeInSourceStats();

    attackSpeed = Utils.calculateAttackRate(sourceStats.getUnitType(), sourceStats.getAgility());
    nextAttackTime = 0.0f;
  }

  public virtual bool shouldExecute()
  {
    bool execute = Time.time > nextAttackTime && acquireTarget();
    if (execute)
    {
      resetCooldown();
    }
    return execute;
  }

  public virtual void executeBehavior()
  {
    fireMissile();
  }


  private bool acquireTarget()
  {
    if (isCurrentTargetInRange())
    {
      return true;
    }
    else
    {
      return acquireNewTarget();
    }
  }

  public virtual void resetCooldown()
  {
    nextAttackTime = Time.time + attackSpeed;
  }

  private void fireMissile()
  {
    if (currentTarget != null)
    {
      playAnimation();
      spawnMissile();
    }
  }

  private void playAnimation()
  {
    if (animator != null)
    {
      animator.Play(animationName);
    }
    else
    {
      Debug.Log(transform.name + " is missing an animator!");
    }
  }

  private void spawnMissile()
  {
    if (missilePrefab != null)
    {
      Stats targetStats = currentTarget.transform.GetComponent<Stats>();
      int damage = Utils.calculateDamageDealt(targetStats, sourceStats.getAttack(), sourceStats.getMagic());
      Transform missile = Instantiate(missilePrefab, transform.position, transform.rotation);
      missile.GetComponent<Missile>().setTarget(currentTarget.transform);
      missile.GetComponent<Missile>().setDamage(damage);
      missile.GetComponent<Missile>().setSourceStats(sourceStats);
    }
    else
    {
      Debug.Log(transform.name + " is missing a projectile prefab!");
    }
  }

  private bool acquireNewTarget()
  {
    List<GameObject> targetsInRange = Utils.getTaggedObjectsInRange(transform.position, range, targetAlignment.ToString());
    //Debug.Log("Targets in range:"+enemiesInRange.Count);
    if (targetsInRange.Count > 0)
    {
      //Debug.Log("Acquiring new target! + " + enemiesInRange[0].transform.name);
      currentTarget = targetsInRange[0];
      return true;
    }
    else
    {
      currentTarget = null;
      return false;
    }
  }

  private float findRangeInSourceStats()
  {
    if (sourceStats is HeroStats)
    {
      return (sourceStats as HeroStats).getRange();
    }
    //todo ian.harris 10/17/2019 add enemy ranged stats when they exist
    else
    {
      Debug.Log("Range not found on source stats! Did you attach this behavior to the right unit?");
      return 0;
    }
  }

  private bool isCurrentTargetInRange()
  {
    if (currentTarget != null)
    {
      return Vector3.Distance(currentTarget.transform.position, transform.position) < range;
    }
    else
    {
      return false;
    }
  }
}
