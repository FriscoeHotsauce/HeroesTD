using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToGoal : MonoBehaviour
{
    public Transform goal;
    private float moveSpeed;
    public float attackSpeed;
    public int attack;
    public bool blockable;
    public bool isBlocked;
    public int damageToGoal;
    public GameObject targetToAttack;
    private float nextAttackTime;
    private Animator animator;

    private NavMeshAgent agent;
    private EnemyMeleeStats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        if(goal == null){
            goal = GameObject.FindGameObjectWithTag("Goal").transform;
        }
        damageToGoal = 1;
        //scrape stats
        enemyStats = GetComponent<EnemyMeleeStats>();
        moveSpeed = enemyStats.getMovement();
        attackSpeed = Utils.calculateAttackRate(enemyStats.GetUnitType(), enemyStats.getAgility());
        attack = enemyStats.getAttack();
        nextAttackTime = 0.0f;

        //set up nav mesh
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        agent.speed = moveSpeed;

        //set animator
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        if(isInRangeOfGoal()){
            goal.GetComponent<BaseHealthBar>().dealDamage(damageToGoal);
            Destroy(gameObject);
        } else if (targetToAttack != null && Time.time > nextAttackTime){
            attackTarget();
        }
    }

    private void attackTarget(){
        int damage = Utils.calculateDamageDealt(targetToAttack.GetComponent<Stats>(), attack, enemyStats.getMagic());
        if(targetToAttack.GetComponent<HealthBar>().dealDamage(damage)){
            unblockEnemy();
        }
        nextAttackTime = Time.time + attackSpeed;
        animator.Play("Bite");
    }

    private bool isInRangeOfGoal(){
        return Vector3.Distance(goal.transform.position, transform.position) < 1.0f;
    }
    public bool isBlockable(){
        return blockable && !isBlocked;
    }

    public void blockEnemy(GameObject blocker){
        //Debug.Log("Enemy "+transform.name+"-- I am being blocked!");
        targetToAttack = blocker;
        agent.speed = 0;
        isBlocked = true;
    }

    public void unblockEnemy(){
        agent.speed = moveSpeed;
        isBlocked = false;
        targetToAttack = null;
    }


}
