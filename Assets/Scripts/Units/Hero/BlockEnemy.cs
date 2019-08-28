using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{

    public float range;
    public int attack;
    public float attackSpeed;
    public int block;
    public int currentlyBlocking;
    public List<GameObject> engagedEnemies;
    private float nextAttackTime;
    public Animator animator;


    //a higher agility should mean a faster attack rate, but we want the number *increasing* because it feels better. 
    //An agility of 50 = 1 attack per second. Maybe try and find a better formula for this.
    public float calculateAttackRate(float agility){
        return (50 / agility);
    }
    void Start(){
        //initialize stats
        attack = GetComponent<Stats>().getAttack();
        attackSpeed = calculateAttackRate(GetComponent<Stats>().getAgility());
        block = GetComponent<Stats>().getBlock();
        range = GetComponent<Stats>().getRange();
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
        List<GameObject> enemiesInRange = getEnemiesInRange();
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

    private bool performAttack(GameObject gameObject){
        animator.Play("Swipe");
        return gameObject.GetComponent<HealthBar>().dealDamage(attack);
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
