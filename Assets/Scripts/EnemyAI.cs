using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // defines how the Enemy will be acting
    // most of the code comes from LittleByte Game Studio - https://www.youtube.com/watch?v=_0lUbScDi6o&t=1254s

    NavMeshAgent agent;
    Transform target;
    public Animator anim;
    bool isDead = false;
    public bool canAttack = true;
    [SerializeField]
    float chaseDistance = 2f;
    [SerializeField]
    float turnSpeed = 5f;
    public float damageAmount = 35f;
    [SerializeField]
    float attackTime = 2f;

    public AudioSource source;
    public AudioClip growlingSound;

    // defining who's the agent (enemy) and who's going to be the target who is tagged as "Player"

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // if the distance is bigger to the player, than the chaseDistance (defined on top) then the function ChasePlayer gets activated
    // If the player is not dead and the enemy canAttack, then it should attack the Player
    // if the Player is dead, the enemy should be disbaled

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance > chaseDistance && !isDead)
        {
            ChasePlayer();


        }
        else if(canAttack && !PlayerHealth.singleton.isDead)
        {
            AttackPlayer();
           
        }
        else if (PlayerHealth.singleton.isDead)
        {
            DisableEnemy();
        }
    }

    // the death animation will start, when Enemy is dead

    public void EnemyDeathAnim()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        source.Stop();
    }

    // will chase the Player and will be playing walking and attacking animations

    private void ChasePlayer()
    {
        agent.updatePosition = true;
        agent.SetDestination(target.position);
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
    }

    // attacks the Player and it's always facing the Player


    private void AttackPlayer()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.updatePosition = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
        StartCoroutine(AttackTime());

    }

    // when DisableEnemy-function is triggered, Enemy can't attack and no animation anymore

    void DisableEnemy()
    {
        canAttack = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
    }

    // wait 0,5s for every attack, until Enemy can attack anymore

    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerHealth.singleton.PlayerDamage(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;

    }
}
