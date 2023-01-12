using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        EnemyManager EnemyManager;
        EnemyStats enemyStats;
        
        private void Awake()
        {
            anim = GetComponent<Animator>();
            EnemyManager = GetComponentInParent<EnemyManager>();
            enemyStats = GetComponentInParent<EnemyStats>();
            
        }

        public void AwardSoulsOnDeath()
        {

            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            SoulsCounter soulsCounter = FindObjectOfType<SoulsCounter>();

            //add souls uppon killing an enemy
            if (playerStats != null)
            {
                playerStats.AddSouls(enemyStats.soulsRewardOnDeath);

                if (soulsCounter != null)
                {
                    soulsCounter.setSoulCount(playerStats.soulCount);
                }
            }

            
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            EnemyManager.enemyRigidBody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            EnemyManager.enemyRigidBody.velocity = velocity;

            if (EnemyManager.isRotatingWithRootMotion)
            {
                EnemyManager.transform.rotation *= anim.deltaRotation;
            }


        }

        public void CanRotate()
        {
            anim.SetBool("canRotate", true);
        }
        public void StopRotation()
        {
            anim.SetBool("canRotate", false);
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }

        public void EnableInvunerable()
        {
            anim.SetBool("isInvunerable", true);
        }
        public void disableInvunerable()
        {
            anim.SetBool("isInvunerable", false);
        }
    }
}

