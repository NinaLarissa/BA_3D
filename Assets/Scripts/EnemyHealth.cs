using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Script defines Enemies Health
    // most of the code comes from LittleByte Game Studio - https://www.youtube.com/watch?v=YigFMim9JTU&t=1s
    public float enemyHealth = 20f;
    EnemyAI enemyAI;
    public bool isEnemyDead;


    // Access to EnemyAI script
    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();


    }

    // deductHealth is defined in the SimpleShoot-Script
    // IF Enemy is not dead, it's possible to deductHealth with the pistol
    // Play EnemyDead-Function it Enemies health is 0
    public void DeductHealth(float deductHealth)
    {
        if (!isEnemyDead)
        {
            enemyHealth -= deductHealth;



            if (enemyHealth <= 0)
            {
                EnemyDead();
                //  source.Stop();
            }
        }

    }

    // When function is played, enemy is dead and can't attack. The death animation will be played and the object will disappear after 10s
    private void EnemyDead()
    {
        isEnemyDead = true;
        enemyAI.canAttack = false;
        enemyAI.EnemyDeathAnim();
        Destroy(gameObject, 10);
    }
}

