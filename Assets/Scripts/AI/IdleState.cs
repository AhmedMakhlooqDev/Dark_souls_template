using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class IdleState : State
    {
        public PersueTargetState persueTargetState;

        public LayerMask detectionLayer;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            #region Handle Enemy Target Detection
            //look for a potential Target (Player)
            // Switch to the Persue state if the Target(Player) has been Found
           

            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            //search for players through colliders
            for (int i = 0; i < colliders.Length; i++)
            {

                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
                //Player has been Detected
                if (characterStats != null)
                {
                    // check for team ID , is a player?
                    
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    //update rotation to face the the player 
                    if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                          enemyManager.currentTarget = characterStats;
                        HandleRotateTowardsTarget(enemyManager);
                        

                    }

                }
            }
            #endregion

            #region Handle Switching to next State
            if (enemyManager.currentTarget != null)
            {
                return persueTargetState;
            }
            else
            {
                return this;
            }
            #endregion

           
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //handle rotation while performing actions
            if (enemyManager.isPerformingAction)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();
                
                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);

            }
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidBody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }


        }
    }

}
