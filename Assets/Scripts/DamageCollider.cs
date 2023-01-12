
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        public int currentWeaponDamage = 25;
        PlayerAttacker playerAttacker;
        WeaponItem weapon;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            damageCollider = GetComponent<Collider>();            
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;

        }
        //enable and disable colliders on the animation events
        //give any collider the abillty to deal damage
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;

        }
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider collision)
        {
            if(collision.tag == "Player")
            {
               PlayerStats playerStats = collision.GetComponent<PlayerStats>();
                

                if(playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                }
            }

            if(collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {                  
                    enemyStats.TakeDamage(currentWeaponDamage);
                }
            }

            if (collision.tag == "Boss")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamageBoss(currentWeaponDamage);
                }
            }
        }
    }
}

