using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public EnemyAttackAction[] enemyAttacks;
        public PersueTargetState persueTargetState;

        bool randomDestinationSet = false;
        float verticalMovementValue = 0;
        float horizontalMovementValue = 0;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            enemyAnimatorManager.anim.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemyAnimatorManager.anim.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
            attackState.hasPerformedAttack = false;


            if (enemyManager.isInteracting)
            {

                enemyAnimatorManager.anim.SetFloat("Vertical", 0);
                enemyAnimatorManager.anim.SetFloat("Horizontal", 0);
                return this;

            }

            if (distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                return persueTargetState;
            }


            if (!randomDestinationSet)
            {
                randomDestinationSet = true;
                DecideCirclingAction(enemyAnimatorManager);
            }

            HandleRotateTowardsTarget(enemyManager);
                     
            if(enemyManager.currentRecoveryTime <= 0 && attackState.currentAttack != null)
            {
                randomDestinationSet = false;
                return attackState;
            }
            else
            {
                GetNewAttack(enemyManager);
            }

            return this;
        }

              

        private void GetNewAttack(EnemyManager enemyManager)
        {
            Vector3 targetsDirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;
            // loop through the attacks list assigned to the ai in the AI attacks and retrieve them while incresing the score 
            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;

                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;
            // loop through the attacks list assigned to the ai in the AI attacks and retrieve a random attack to play it
            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {

                        if (attackState.currentAttack != null)
                            return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            attackState.currentAttack = enemyAttackAction;

                        }
                    }
                }
            }



        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //handle rotation while performing actions
            if (enemyManager.isPreformingAction)
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

        private void DecideCirclingAction(EnemyAnimatorManager enemyAnimatorManager)
        {
            WalkAroundTarget(enemyAnimatorManager);
        }
        private void WalkAroundTarget(EnemyAnimatorManager enemyAnimatorManager)
        {

            print("This is working");
            //verticalMovementValue = 0.5f;
            verticalMovementValue = Random.Range(-0.5f, 0.5f);

            if (verticalMovementValue <= 1 && verticalMovementValue > -0.5f)
            {
                verticalMovementValue = 0.5f;
            }
            else if (verticalMovementValue >= -1 && verticalMovementValue < 0)
            {
                verticalMovementValue = -0.5f;
            }


            horizontalMovementValue = Random.Range(-0.5f, 1);

            if (horizontalMovementValue <= 1 && horizontalMovementValue >= -0.5f)
            {
                horizontalMovementValue = 0.5f;
            }
            else if (horizontalMovementValue >= -1 && horizontalMovementValue < 0)
            {
                horizontalMovementValue = -0.5f;
            }
        }
    }
}

