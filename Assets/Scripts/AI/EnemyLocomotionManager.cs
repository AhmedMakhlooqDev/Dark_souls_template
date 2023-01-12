using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AM
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;       
        public LayerMask detectionLayer;
                          
        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();                       
        }

    }


}
