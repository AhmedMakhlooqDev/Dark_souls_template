using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace AM
{
    public class EnemyManager : CharachterManager
    {
        
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimationManager;
        EnemyStats enemyStats;

        public bool isPreformingAction;
        public bool isInteracting;

        public State currentState;
        public CharacterStats currentTarget;
        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidBody;

        [Header("AI Settings")]
        public float detectionRadius = 20;
        //AI EYESIGHT DETECTION ANGLE
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;
        public float currentRecoveryTime = 0;
        public float rotationSpeed = 15;
        public float maximumAggroRadius = 1.5f;
        public float viewableAngle;

        [Header("Combat Flags")]
        public bool canDoCombo;

        [Header("AI combat settings")]
        public bool allowToPerformCombos;
        public float comboLikelyHood;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyStats = GetComponent<EnemyStats>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            navMeshAgent.enabled = false;
            enemyRigidBody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            enemyRigidBody.isKinematic = false;
        }

        private void Update()
        {
            HandleRecoveryTime();
            HandleStateMachine();

            isRotatingWithRootMotion = enemyAnimationManager.anim.GetBool("isRotatingWithRootMotion");
            canDoCombo = enemyAnimationManager.anim.GetBool("canDoCombo");
            isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
            canRotate = enemyAnimationManager.anim.GetBool("canRotate");
        }
        private void LateUpdate()
        {           
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;
        }
        private void HandleStateMachine()
        {
            if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);

                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
            
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        //give the AI recovery time between his attacks to cooldown
        private void HandleRecoveryTime()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPreformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPreformingAction = false;
                }
            }
        }

        
    }


}

