using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
  public float range;
  public int attack;
  public float attackSpeed;
  public Transform missilePrefab;
  public GameObject currentTarget;
  private float nextAttackTime;

    void Start(){
        attack = GetComponent<HeroStats>().getAttack();
        attackSpeed = calculateAttackRate(GetComponent<HeroStats>().getAgility());
        range = GetComponent<HeroStats>().getRange();

        nextAttackTime = 0.0f;
    }

    void FixedUpdate(){
        if(Time.time > nextAttackTime){
            acquireAndAttackTarget();
            nextAttackTime = Time.time + attackSpeed;
        }
    }

    private void acquireAndAttackTarget(){
        if(isCurrentTargetInRange()){
            fireMissile();
        } else {
            //Debug.Log("No target. Attempting to acquire new target.");
            acquireNewTarget();
            fireMissile();
        }

    }

    private void acquireNewTarget(){
        List<GameObject> enemiesInRange = getEnemiesInRange();
        //Debug.Log("Targets in range:"+enemiesInRange.Count);
        if(enemiesInRange.Count > 0){
            //Debug.Log("Acquiring new target! + " + enemiesInRange[0].transform.name);
            currentTarget = enemiesInRange[0];
        } else {
            currentTarget = null;
        }
    }

    private void fireMissile(){
        if(currentTarget != null){
            //Debug.Log("Bang! - " + currentTarget.transform.name);
            Transform missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            
            missile.GetComponent<Missile>().setTarget(currentTarget.transform);
            missile.GetComponent<Missile>().setDamage(attack);
        }
        
    }

    private bool isCurrentTargetInRange(){
        if(currentTarget != null){
            return Vector3.Distance(currentTarget.transform.position, transform.position) < range;
        } else {
            return false;
        }
    }

    //a higher agility should mean a faster attack rate, but we want the number *increasing* because it feels better. 
    //An agility of 40 = 1 attack per second. Maybe try and find a better formula for this.
    public float calculateAttackRate(float agility){
        return (40 / agility);
    }

    private List<GameObject> getEnemiesInRange(){
        List<GameObject> enemiesInRange = new List<GameObject>();
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in allEnemies){
            if(Vector3.Distance(enemy.transform.position, transform.position) < range){
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }
}
