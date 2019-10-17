using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missile : MonoBehaviour
{

  public Transform targetTransform;
  public Vector3 lastKnownPosition;
  public HeroStats heroStats;

  public float smooth;
  public float speed;
  public int missileDamage;

  void Update()
  {
    if (targetTransform != null)
    {
      moveTowardsTarget();
    }
    else
    {
      endMissile();
    }
  }

  /*
    Defines the behaviour the missile should take while the target is alive
   */
  private void moveTowardsTarget()
  {
    transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * speed);
    transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, Time.deltaTime * speed);
    lastKnownPosition = targetTransform.position;
    if (isHit())
    {
      dealDamage();
    }
  }

  /*
    Defines the behaviour the missile should take if the target is killed by another source
   */
  public virtual void endMissile()
  {
    transform.position = Vector3.Lerp(transform.position, lastKnownPosition, Time.deltaTime * speed);
    if (Vector3.Distance(lastKnownPosition, transform.position) < .5f)
    {
      Destroy(gameObject);
    }
  }

  private bool isHit()
  {
    return Vector3.Distance(targetTransform.position, transform.position) < 1f;
  }

  public virtual void dealDamage()
  {
    if (targetTransform.GetComponent<HealthBar>().dealDamage(missileDamage))
    {
      heroStats.addExperience(targetTransform.GetComponent<Stats>().getExperience());
    }
    Destroy(gameObject);
  }

  public void setTarget(Transform target)
  {
    //Debug.Log("SETTING TARGET - "+target.name);
    targetTransform = target;
    //Debug.Log(targetTransform != null);
  }

  public void setDamage(int damage)
  {
    missileDamage = damage;
  }

  public void setHeroStats(HeroStats heroStats)
  {
    this.heroStats = heroStats;
  }
}
