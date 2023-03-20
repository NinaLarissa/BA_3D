using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    //Defines the health of the player and respawns him to the beginning of the level again, 
    // when their health is at 0
    // most of the code comes from LittleByte Game Studio - https://www.youtube.com/watch?v=Jb1xipajc-E


    public static PlayerHealth singleton;
    public float currentHealth;
    public float maxHealth = 100;
    public bool isDead = false;
    public int Respawn;


    private void Awake()
    {
        singleton = this;
    }

    // when level starts, currentHealth is the same as maxHealth

    void Start()
    {
        currentHealth = maxHealth;
    }

    // if currentHealth is smaller than 0, put it to 0
    void Update()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    // if currentHealth is more than 0 but gets attacked, deduct health, when health is 0, player play Dead-function
    // damage amout is via the EnemyAI script
    public void PlayerDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            Dead();
        }
    }

    // Player is dead, when currentHealth is 0. Player will be transported to the beginning of the level
    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        Debug.Log("Player Is Dead");
        SceneManager.LoadScene(Respawn);
    }

}

