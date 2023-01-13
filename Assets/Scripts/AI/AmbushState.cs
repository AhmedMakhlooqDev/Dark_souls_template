using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    /* this state has been written just in case we need it, at the moment it is not implemented in the animator or AI*/
    public class AmbushState : State
    {
        public bool isSleeping;
        public float detectionRadius;
        public string sleepAnimation;
        public string wakeAnimation;

        public LayerMask detectionLayer;

        public PersueTargetState persueTargetState;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (isSleeping && enemyManager.isInteracting == false)
            {
                enemyAnimatorManager.PlayerTargetAnimation(sleepAnimation, true);
            }

            #region Handle target detection
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    Vector3 targetsDirection = characterStats.transform.position - enemyManager.transform.position;
                    enemyManager.viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);

                    if (enemyManager.viewableAngle > enemyManager.minimumDetectionAngle
                        && enemyManager.viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                        isSleeping = false;
                        enemyAnimatorManager.PlayerTargetAnimation(wakeAnimation, true);
                    }
                }
            }
            #endregion

            #region handle State change

            if(enemyManager.currentTarget != null)
            {
                return persueTargetState;
            }
            else
            {
                return this;
            }

            #endregion
        }
    }
}