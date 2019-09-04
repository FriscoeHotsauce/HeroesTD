using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealAllies : MonoBehaviour
{
    public enum BehaviourState { Attack, Heal }
    public float attackSpeed;
    public GameObject currentTarget;
    public List<GameObject> alliesInRange;
    public int overheal;
    public Transform missilePrefab;
    private HeroStats heroStats;
    private float nextAttackTime;

    void Start(){
        heroStats = GetComponent<HeroStats>();
        attackSpeed = Utils.calculateAttackRate(heroStats.getUnitType(), heroStats.getAgility());
        nextAttackTime = 0.0f;
    }

    void FixedUpdate(){

        if(Time.time > nextAttackTime){
            acquireTarget();
            if(currentTarget != null){
                healOrDamage();
            }
            nextAttackTime = Time.time + attackSpeed;
        }
    }

    private void acquireTarget(){
        List<GameObject> alliesInRange = Utils.getAlliesInRange(transform.position, heroStats.getRange());
        //find targets with a health less than maxhealth + overheal
        currentTarget = alliesInRange.FirstOrDefault(ally => !ally.GetComponent<HealthBar>().isMaxHealth(overheal));
        //if none, attack nearest enemies
        if(currentTarget == null){
            List<GameObject> enemiesInRange = Utils.getEnemiesInRange(transform.position, heroStats.getRange());
            if(enemiesInRange.Count > 0){
                currentTarget = enemiesInRange[0]; 
            } else {
                currentTarget = null;
            }
        }
    }

    private void healOrDamage(){
        if(currentTarget.tag == "Hero"){
            Debug.Log("Healing!");
            currentTarget.GetComponent<HealthBar>().heal(heroStats.getMagic(), overheal);
        } else {
            Debug.Log("Attacking!");
            fireMissile();
        }
    }

     private void fireMissile(){
        if(currentTarget != null){            
            Stats enemyStats = currentTarget.transform.GetComponent<Stats>();
            int damage = Utils.calculateDamageDealt(enemyStats, heroStats.getAttack(), heroStats.getMagic());
            
            Transform missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            missile.GetComponent<Missile>().setTarget(currentTarget.transform);
            missile.GetComponent<Missile>().setDamage(damage);
        }
    }
}
