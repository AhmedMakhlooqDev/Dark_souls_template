using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class RotateTowardsTargetState : State
    {
        public CombatStanceState combatStanceState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {


            enemyAnimatorManager.anim.SetFloat("Vertical", 0);
            enemyAnimatorManager.anim.SetFloat("Horizontal", 0);

            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

            //Debug.Log("veiwableAngle" + viewableAngle);

            if (enemyManager.isInteracting)
                return this; //When we enter the state we will still be interacting from the attack animation so we pause here until it has finished

            if (viewableAngle >= 100 && viewableAngle <= 180 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootRotation("TurnBehind", true);
                return combatStanceState;
            }
            else if (viewableAngle <= -101 && viewableAngle >= -180 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootRotation("TurnBehind", true);
                return combatStanceState;
            }
            else if (viewableAngle <= -35 && viewableAngle >= -90 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootRotation("TurnRight", true);
                return combatStanceState;
            }
            else if (viewableAngle >= 35 && viewableAngle <= 90 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootRotation("TurnLeft", true);
                return combatStanceState;
            }

            return combatStanceState;
        }
    }
}

