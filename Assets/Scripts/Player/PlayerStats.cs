using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AM
{
    public class PlayerStats : CharacterStats
    {

        PlayerManager playerManager;
        public HealthBar healthBar;
        AnimatorHandler animatorHandler;
        public GameObject youDied;
        public AudioSource deathSFX;
        public AudioSource deathScreamSFX;
        Collider collider;
        
        public float healingTime;

        [SerializeField]
        private AudioClip[] clips;

        private AudioSource audioSource;

        private void Update()
        {
            Debug.Log(healingTime);
            if (Time.time - healingTime > 3f)
            {
                giveHealth();
                
            }
            
           
           
        }

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            audioSource = GetComponent<AudioSource>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            collider = GetComponent<Collider>();
        }

        // Start is called before the first frame update
        void Start()
        {
            healingTime = 0f;
            maxHealth = setMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private int setMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 100;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            //rolling gives the player invincibility frames
            if (playerManager.isInvunerable)
                return;

            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);
            animatorHandler.PlayerTargetAnimation("Hit", true);
            //player Die
            if(currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        public void giveHealth()
        {       
           // restore player health every 3 seconds
           if(currentHealth <= 1950)
            {
                currentHealth += 100;
                healthBar.SetCurrentHealth(currentHealth);
               
                healingTime = Time.time;
            }
            
                     
        }

        private void HandleDeath()
        {
            currentHealth = 0;
            animatorHandler.PlayerTargetAnimation("Player Die", true);
            youDied.SetActive(true);
            deathSFX.Play();
            deathScreamSFX.Play();
            collider.enabled = false;
        }

        public void AddSouls(int souls)
        {
            soulCount = soulCount + souls;
        }



    }
}

