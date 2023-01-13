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

            if (enemyManager.isInteracting)
                return this;

            if(viewableAngle >= 100 && viewableAngle <= 180 && !enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootMotion("TurnBehind", true);
                return combatStanceState;
            }
            else if (viewableAngle >= -101 && viewableAngle <= -180 && !enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootMotion("TurnBehind", true);
                return combatStanceState;

            }
            else if (viewableAngle >= -45 && viewableAngle <= -100 && !enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootMotion("TurnRight", true);
                return combatStanceState;

            }
            else if (viewableAngle >= 45 && viewableAngle <= 100 && !enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootMotion("TurnLeft", true);
                return combatStanceState;

            }
            return combatStanceState;
        }
    }
}

