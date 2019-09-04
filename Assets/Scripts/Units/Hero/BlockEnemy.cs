using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{

    public int attack;
    public float attackSpeed;
    public int block;
    public int currentlyBlocking;
    public List<GameObject> engagedEnemies;
    private HeroStats heroStats;
    private float nextAttackTime;
    public Animator animator;

    void Start(){
        //initialize stats
        heroStats = GetComponent<HeroStats>();
        attack = heroStats.getAttack();
        attackSpeed = Utils.calculateAttackRate(heroStats.getUnitType(), heroStats.getAgility());
        block = heroStats.getBlock();
        animator = GetComponent<Animator>();

        engagedEnemies = new List<GameObject>();
        nextAttackTime = 0.0f;
    }

    //death is handled by the healthbar; do cleanup here
    void OnDestroy(){
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
            
        if(enemiesInRange.Count > 0 && currentlyBlocking < block){
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
            if(currentlyBlocking == block){
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
        animator.Play("Swipe");
        int damage = Utils.calculateDamageDealt(enemyStats, attack, heroStats.getMagic());
        return gameObject.GetComponent<HealthBar>().dealDamage(damage);
    }

    private void attackEngagedEnemies(){
        if(performAttack(engagedEnemies[0])){
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
