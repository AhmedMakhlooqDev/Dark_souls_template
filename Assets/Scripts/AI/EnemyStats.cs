using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AM
{
    public class EnemyStats : CharacterStats
    {
        public UIEnemyHealthBar enemyHealthBar;
        CameraHandler cameraHandler;
        InputHander inputHander;
        public AudioSource audioSource;
        public Collider collider;
        NavMeshAgent navMeshAgent;
        public int soulsRewardOnDeath;
        EnemyAnimatorManager enemyAnimatorManager;


        private void Awake()
        {

            cameraHandler = GetComponentInChildren<CameraHandler>();
            inputHander = GetComponentInChildren<InputHander>();           
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        }

        // Start is called before the first frame update
        void Start()
        {

            maxHealth = setMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            enemyHealthBar.SetMaxHealth(maxHealth);
            
        }

        private int setMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;
            
            currentHealth = currentHealth - damage;
            enemyHealthBar.SetHealth(currentHealth);
            
            enemyAnimatorManager.PlayerTargetAnimation("Hit", true);

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }


        public void TakeDamageBoss(int damage)
        {
            if (isDead)
                return;

            currentHealth = currentHealth - damage;
            enemyHealthBar.SetHealth(currentHealth);
            audioSource.Play();

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            isDead = true;
            currentHealth = 0;
            //set death animation playing to true
            enemyAnimatorManager.PlayerTargetAnimation("Die", true);
            GetComponent<EnemyManager>().enabled = false;
            Destroy(gameObject, 10f);

        }
    }

}
