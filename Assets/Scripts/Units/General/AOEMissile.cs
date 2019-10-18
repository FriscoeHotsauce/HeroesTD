using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEMissile : Missile
{
  public float aoeRange;
  public Transform particleSystemPrefab;

  public override void dealDamage()
  {
    Utils.getEnemiesInRange(lastKnownPosition, aoeRange).ForEach(enemy =>
    {
      if (enemy.GetComponent<HealthBar>().dealDamage(missileDamage))
      {
        applyExperienceIfHero();
      }
    });
    explode();
  }

  //AOE missiles should continue to their intended target and explode from that point
  public override void endMissile()
  {
    transform.position = Vector3.Lerp(transform.position, lastKnownPosition, Time.deltaTime * speed);
    if (Vector3.Distance(lastKnownPosition, transform.position) < .5f)
    {
      dealDamage();
    }
  }

  private void explode()
  {
    Instantiate(particleSystemPrefab, transform.position, transform.rotation);
    Destroy(gameObject);
  }
}
