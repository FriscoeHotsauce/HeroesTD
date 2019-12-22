using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeleeAttack : MonoBehaviour, TimedBehavior
{

  public string attackAnimationName;

  private GameObject currentTarget;
  private HeroStats heroStats;
  private Animator animator;
  private float range;
  private float nextAttackTime;
  private MeleeBlock meleeBlock;

  void Start()
  {
    animator = GetComponent<Animator>();
    heroStats = GetComponent<HeroStats>();
    range = heroStats.getRange();
    meleeBlock = GetComponent<MeleeBlock>();

    nextAttackTime = 0.0f;
  }

  public void resetCooldown()
  {
    nextAttackTime = Time.time + calculateAttackSpeed();
  }

  public bool shouldExecute()
  {
    bool execute = Time.time > nextAttackTime && acquireTarget();
    if (execute)
    {
      resetCooldown();
    }
    return execute;
  }

  public void executeBehavior()
  {
    attackTarget();
  }

  public bool acquireTarget()
  {

    if (currentTarget != null)
    {
      return true;
    }
    else if (meleeBlock == null)
    {
      currentTarget = Utils.getEnemiesInRange(transform.position, heroStats.getRange()).First();
      return currentTarget != null;
    }
    else if (meleeBlock.getEnagedEnemies().Any())
    {
      currentTarget = meleeBlock.getEnagedEnemies().First();
      return currentTarget != null;
    }
    else
    {
      return false;
    }
  }

  private void attackTarget()
  {
    if (currentTarget != null)
    {
      playAnimation();
      int damage = Utils.calculateDamageDealt(heroStats, heroStats.getAttack(), heroStats.getMagic());
      if (currentTarget.GetComponent<HealthBar>().dealDamage(damage))
      {
        heroStats.addExperience(currentTarget.GetComponent<Stats>().getExperience());
      }
    }
  }

  private void playAnimation()
  {
    if (animator != null)
    {
      animator.Play(attackAnimationName);
    }
    else
    {
      Debug.Log(transform.name + " is missing an animator!");
    }
  }

  private float calculateAttackSpeed()
  {
    return Utils.calculateAttackRate(heroStats.getUnitType(), heroStats.getAgility());
  }
}
