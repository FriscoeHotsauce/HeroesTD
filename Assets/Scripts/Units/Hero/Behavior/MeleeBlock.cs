using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeBlock : MonoBehaviour, BlockBehavior
{

  private List<GameObject> engagedEnemies;
  private int currentlyBlocking;
  private HeroStats heroStats;

  void Start()
  {
    heroStats = GetComponent<HeroStats>();
    engagedEnemies = new List<GameObject>();
  }

  public bool shouldExecute()
  {
    return true;
  }

  public void executeBehavior()
  {
    sterilizeEngagedEnemies();
    if (currentlyBlocking < heroStats.getBlock())
    {
      List<GameObject> enemiesInRange = Utils.getEnemiesInRange(transform.position, heroStats.getRange());
      if (enemiesInRange.Any())
      {
        tryToBlockEnemy(enemiesInRange);
      }
    }
  }

  public List<GameObject> getEnagedEnemies()
  {
    return engagedEnemies;
  }

  //death is handled by the healthbar; do cleanup here
  void OnDestroy()
  {
    sterilizeEngagedEnemies();
    foreach (GameObject enemy in engagedEnemies)
    {
      enemy.GetComponent<MoveToGoal>().unblockEnemy();
    }
  }

  private void tryToBlockEnemy(List<GameObject> enemies)
  {
    foreach (GameObject enemy in enemies)
    {
      if (currentlyBlocking == heroStats.getBlock())
      {
        break;
      }
      else if (enemy.GetComponent<MoveToGoal>().isBlockable())
      {
        enemy.GetComponent<MoveToGoal>().blockEnemy(gameObject);
        engagedEnemies.Add(enemy);
        currentlyBlocking = currentlyBlocking + 1;
      }
    }

  }

  //Clean out enemies destroyed by other units, update block count
  private void sterilizeEngagedEnemies()
  {
    engagedEnemies.RemoveAll(enemy => enemy == null);
    currentlyBlocking = engagedEnemies.Count;
  }
}
