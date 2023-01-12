using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class CameraHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        InputHander inputHandler;
        PlayerManager playerManager;

        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        public LayerMask ignoreLayers;
        public LayerMask environmentLayer;
        private Vector3 cameraFollowVelocity = Vector3.zero;

        public static CameraHandler singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float targetPosition;
        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35;
        public float maximumPivot = 35;
        

        
        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffSet = 0.2f;
        public float minimumCollisionOffset = 0.2f;
        public float lockedPivotPostion = 2.25f;
        public float unlockedPivotPostion = 1.65f;

        public CharachterManager currentLockOnTarget;

        List<CharachterManager> availableTargets = new List<CharachterManager>();
        public CharachterManager nearestLockOnTarget;
        public CharachterManager leftLockOntarget;
        public CharachterManager rightLockOnTarget;      
        public float maximumLockOnDistance = 30;

        private void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8| 1 << 9 | 1 << 10);
            
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputHandler = FindObjectOfType<InputHander>();
            playerManager = FindObjectOfType<PlayerManager>();
        }
        private void Start()
        {
            environmentLayer = LayerMask.NameToLayer("Environment");
        }
        public void FollowTarget(float delta)
        {
            //calculate taraget position between it and the camera
            Vector3 targetPostion = Vector3.SmoothDamp(myTransform.position, targetTransform.position,ref cameraFollowVelocity, delta / followSpeed);
            myTransform.position = targetPostion;
            HandleCameraCollisions(delta);
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            if(inputHandler.lockOnFlag == false && currentLockOnTarget == null)
            {
                //handle camera rotation speed according to the input controller sensitivity
                lookAngle += (mouseXInput * lookSpeed) / delta;
                pivotAngle -= (mouseYInput * pivotSpeed) / delta;
                pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

                Vector3 rotation = Vector3.zero;
                rotation.y = lookAngle;
                Quaternion targetRotation = Quaternion.Euler(rotation);
                myTransform.rotation = targetRotation;

                rotation = Vector3.zero;
                rotation.x = pivotAngle;

                targetRotation = Quaternion.Euler(rotation);
                cameraPivotTransform.localRotation = targetRotation;
            }
            else
            {
                float velocity = 0;
                // handle rotation calculation if the player is locked on the Enemy
                Vector3 dir = currentLockOnTarget.transform.position - transform.position;
                dir.Normalize();
                dir.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = targetRotation;

                dir = currentLockOnTarget.transform.position - cameraPivotTransform.position;
                dir.Normalize();

                targetRotation = Quaternion.LookRotation(dir);
                Vector3 eulerAngle = targetRotation.eulerAngles;
                eulerAngle.y = 0;
                cameraPivotTransform.localEulerAngles = eulerAngle;
                checkIfLockOnTargetIsDead();
            }

            
            
        }

        private void HandleCameraCollisions(float delta)
        {
            //handle camera postion and looking angle when player is near walls and ground to prevent clipping
            targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();
            // apply clipping if the player is out of the sphere cast radius 
            if(Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition), ignoreLayers))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetPosition = -(dis - cameraCollisionOffSet);

            }
            if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition = -minimumCollisionOffset;
            }
            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
        }

        public void HandleLockOn()
        {
            float shortestDistance = Mathf.Infinity;
            float shortestDistanceOfLeftTarget = -Mathf.Infinity;
            float shortestDistanceOfRightTarget = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharachterManager charachter = colliders[i].GetComponent<CharachterManager>();

                if(charachter != null)
                {
                    Vector3 lockOnTargetFirection = charachter.transform.position - targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.position , charachter.transform.position);
                    float viewableAngle = Vector3.Angle(lockOnTargetFirection, cameraTransform.forward);

                    RaycastHit hit;
                    //handle lockon in the assigned angle
                    if(charachter.transform.root != targetTransform.transform.root 
                        && viewableAngle > -50 && viewableAngle < 50 
                        && distanceFromTarget <= maximumLockOnDistance)
                    {

                        if(Physics.Linecast(playerManager.lockOnTransform.position, charachter.lockOnTransform.position, out hit))
                        {
                            // draw raycast to the Target the player is locking on
                            Debug.DrawLine(playerManager.lockOnTransform.position, charachter.lockOnTransform.position);
                            if(hit.transform.gameObject.layer == environmentLayer)
                            {

                            }
                            else
                            {
                                availableTargets.Add(charachter);
                            }
                        }
                        
                    }

                   

                }
            }

            for (int k = 0; k < availableTargets.Count; k++)
            {
               //handle target lock switching and detection 
               // check of there are multiple targets that you can lock on
                float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);

                if(distanceFromTarget < shortestDistance)
                {
                    shortestDistance = distanceFromTarget;
                    nearestLockOnTarget = availableTargets[k];

                }

                if (inputHandler.lockOnFlag)
                {
                   //handle lock on switching target
                    Vector3 relativeEnemyPosition = inputHandler.transform.InverseTransformPoint(availableTargets[k].transform.position);
                    var distanceFromLeftTarget = relativeEnemyPosition.x;
                    var distanceFromRightTarget = relativeEnemyPosition.x;

                    if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget 
                        && availableTargets[k] != currentLockOnTarget)
                    {
                        shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                        leftLockOntarget = availableTargets[k];
                    }
                    else if (relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget 
                        && availableTargets[k] != currentLockOnTarget)
                    {
                        shortestDistanceOfRightTarget = distanceFromRightTarget;
                        rightLockOnTarget = availableTargets[k];
                    }
                }
            }
        }

        public void ClearLockOnTargets()
        {
            //remove lock on from target
            availableTargets.Clear();
            
            nearestLockOnTarget = null;
            currentLockOnTarget = null;

        }

        public void SetCameraHeight()
        {
            //adjust the camera angle from the inspector 
            //when locked on you can adjust the camera angle to higher or lower to your liking
            Vector3 velocity = Vector3.zero;
            Vector3 newLockedPosition = new Vector3(0, lockedPivotPostion);
            Vector3 newUnlockedPostion = new Vector3(0, unlockedPivotPostion);
            if(currentLockOnTarget != null)
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
            }
            else
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPostion, ref velocity, Time.deltaTime);
            }
        }

        public void checkIfLockOnTargetIsDead()
        {
            //prevent game from crashing when the locked on target has been destroyed
            if (currentLockOnTarget.GetComponentInParent<CharacterStats>().isDead)
            {
                inputHandler.lockOnFlag = true;
                inputHandler.lockOnInput = true;
                inputHandler.HandleLockInput();

            }
        }
    }

}
