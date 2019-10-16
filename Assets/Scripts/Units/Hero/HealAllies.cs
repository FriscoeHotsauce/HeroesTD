using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealAllies : MonoBehaviour
{
    public enum BehaviourState { Attack, Heal }
    public string attackAnimationName;
    public string healAnimationName;
    public float attackSpeed;
    public GameObject currentTarget;
    public List<GameObject> alliesInRange;
    public int overheal;
    public Transform missilePrefab;
    private HeroStats heroStats;
    private float nextAttackTime;
    private Animator animator;

    void Start(){
        heroStats = GetComponent<HeroStats>();
        animator = GetComponent<Animator>();
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
        currentTarget = alliesInRange.FirstOrDefault(ally => !ally.GetComponent<HealthBar>().isMaxHealth(overheal)); //this sucks because once "alliesInRange" is populated the first ally in the list will always be prioritized

        //heal ally with the lowest health //this kinda sucks, revisit this
        // currentTarget = alliesInRange?.Aggregate((lowestHpAlly, otherAlly) => 
        //     lowestHpAlly.GetComponent<HealthBar>().getCurrentHealth() < otherAlly.GetComponent<HealthBar>().getCurrentHealth() ? lowestHpAlly : otherAlly
        // );

        // //clear out current target if they have full health
        // if(currentTarget.GetComponent<HealthBar>().getMaxHealth(overheal) == currentTarget.GetComponent<HealthBar>().getCurrentHealth() + overheal){
        //     currentTarget = null;
        // }


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
            animator.Play(healAnimationName);
            currentTarget.GetComponent<HealthBar>().heal(heroStats.getMagic(), overheal);
            heroStats.addExperience(1);
        } else {
            fireMissile();
        }
    }

     private void fireMissile(){
        if(currentTarget != null){
            animator.Play(attackAnimationName);            
            Stats enemyStats = currentTarget.transform.GetComponent<Stats>();
            int damage = Utils.calculateDamageDealt(enemyStats, heroStats.getAttack(), heroStats.getMagic());
            
            Transform missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            missile.GetComponent<Missile>().setTarget(currentTarget.transform);
            missile.GetComponent<Missile>().setDamage(damage);
            missile.GetComponent<Missile>().setHeroStats(heroStats);
        }
    }
}
