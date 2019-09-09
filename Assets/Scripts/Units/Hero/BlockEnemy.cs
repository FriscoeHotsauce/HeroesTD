using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{

    public string attackAnimation;

    public float attackSpeed;
    private List<GameObject> engagedEnemies;
    private int currentlyBlocking;
    private HeroStats heroStats;
    private float nextAttackTime;
    public Animator animator;

    void Start(){
        //initialize stats
        heroStats = GetComponent<HeroStats>();
        attackSpeed = Utils.calculateAttackRate(heroStats.getUnitType(), heroStats.getAgility());
        animator = GetComponent<Animator>();

        engagedEnemies = new List<GameObject>();
        nextAttackTime = 0.0f;
    }

    //death is handled by the healthbar; do cleanup here
    void OnDestroy(){
        sterilizeEngagedEnemies();
        foreach(GameObject enemy in engagedEnemies){
            enemy.GetComponent<MoveToGoal>().unblockEnemy();
        }
    }

    void FixedUpdate(){
        //clean out enemies that have been killed by other units
        sterilizeEngagedEnemies();
        //fetch and block enemies in range
        List<GameObject> enemiesInRange = Utils.getEnemiesInRange(transform.position,
            heroStats.getRange());
            
        if(enemiesInRange.Count > 0 && currentlyBlocking < heroStats.getBlock()){
            tryToBlockEnemy(enemiesInRange);
        }
        //try to attack enemies
        if(engagedEnemies.Count > 0 && Time.time > nextAttackTime){
            nextAttackTime = Time.time + attackSpeed;
            attackEngagedEnemies();
        }

    }

    private void tryToBlockEnemy(List<GameObject> enemies){
        foreach(GameObject enemy in enemies){
            if(currentlyBlocking == heroStats.getBlock()){
                break;
            } else if(enemy.GetComponent<MoveToGoal>().isBlockable()){
                enemy.GetComponent<MoveToGoal>().blockEnemy(gameObject);
                engagedEnemies.Add(enemy);
                currentlyBlocking = currentlyBlocking + 1;
            }
        }

    }

    private bool performAttack(GameObject gameObject){
        Stats enemyStats = gameObject.GetComponent<Stats>();
        animator.Play(attackAnimation);
        int damage = Utils.calculateDamageDealt(enemyStats, heroStats.getAttack(), heroStats.getMagic());
        return gameObject.GetComponent<HealthBar>().dealDamage(damage);
    }

    private void attackEngagedEnemies(){
        if(performAttack(engagedEnemies[0])){
            heroStats.addExperience(engagedEnemies[0].GetComponent<Stats>().getExperience());
            engagedEnemies.RemoveAt(0);
            currentlyBlocking = currentlyBlocking - 1;
        }
    }

    private void sterilizeEngagedEnemies(){
        for(int i = 0; i < engagedEnemies.Count; i++){
            if(engagedEnemies[i] == null ){
                engagedEnemies.RemoveAt(i);
                currentlyBlocking = currentlyBlocking - 1;
            }
        }
    }
}
