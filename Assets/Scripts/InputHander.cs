using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AM
{
    public class InputHander : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_input;
        public bool rb_input;
        public bool rt_input;
        public bool lb_input;
        public bool d_pad_Up;
        public bool d_pad_Down;
        public bool d_pad_Left;
        public bool d_pad_Right;
        public bool a_input;
        public bool jump_input;
        public bool inventory_input;
        public bool lockOnInput;
        public bool right_Stick_Right_Input;
        public bool right_Stick_Left_Input;


        public bool rollFlag;
        public float rollInputTimer;
        public bool isInteracting;
        public bool sprintFlag;
        public bool comboFlag;
        public bool inventoryFlag;
        public bool lockOnFlag;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        CameraHandler cameraHandler;
        UIManager uIManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        public void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            uIManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
        }



        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
               
                inputActions.PlayerActions.RB.performed += i => rb_input = true;
                inputActions.PlayerActions.RT.performed += i => rt_input = true;
                inputActions.PlayerActions.LB.performed += i => lb_input = true;
                inputActions.PlayerActions.LB.canceled += i => lb_input = false;
                inputActions.PlayerMovement.DPadRight.performed += i => d_pad_Right = true;
                inputActions.PlayerMovement.DPadLeft.performed += i => d_pad_Left = true;
                inputActions.PlayerMovement.AorX.performed += i => a_input = true;
                inputActions.PlayerMovement.Inventory.performed += i => inventory_input = true;
                inputActions.PlayerActions.lockOn.performed += i => lockOnInput = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;
                inputActions.PlayerActions.Jump.performed += i => jump_input = true;


            }

            inputActions.Enable();
        }

        public void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleCombatInput(delta);
            HandleQuickSlotInput();          
            HandleInventoryInput();
            HandleLockInput();
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            b_input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            // if the player only presses the Shift/O buttons he wont sprint
            sprintFlag = b_input;

            if (b_input)
            {
                rollInputTimer += delta;
                
            }
            else
            {
                if(rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }
        }

        private void HandleCombatInput(float delta)
        {

            //assign attack buttons
            if (lb_input)
            {
                
                playerAttacker.HandleLBAction();
            }
            else
            {
                playerManager.isBlocking = false;
            }

            if (rb_input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;
                    if (playerManager.canDoCombo)
                        return;
                    playerAttacker.HandleLightWeightAttack(playerInventory.rightWeapon);

                }

                
            }

            if (rt_input)
            {
                if (playerManager.isInteracting)
                    return;
                if (playerManager.canDoCombo)
                    return;
                playerAttacker.HandleHeavyWeightAttack(playerInventory.rightWeapon);
            }

        }

        private void HandleQuickSlotInput()
        {

            //assign left and right D-pad arrows 
            if (d_pad_Right)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if (d_pad_Left)
            {
                playerInventory.ChangeLeftWeapon();
            }
        }

    

        private void HandleInventoryInput()
        {
            
            if (inventory_input)
            {
                //clicking on the inventory button will display the inventory menu 
                inventoryFlag = !inventoryFlag;
                if (inventoryFlag)
                {
                    uIManager.OpenSelectWindow();
                    uIManager.UpdateUI();
                    uIManager.hudWindow.SetActive(false);
                }
                else
                {      //return to the select window             
                    uIManager.CloseSelectWindow();
                    uIManager.CloseAllInventoryWindows();
                    uIManager.hudWindow.SetActive(true);
                }
            }
            
        }

        public void HandleLockInput()
        {
            if(lockOnInput && lockOnFlag == false)
            {
               
                lockOnInput = false;                      
                cameraHandler.HandleLockOn();
                if(cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if(lockOnInput && lockOnFlag)
            {
                //clear lock on target
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }
            // left and right analog movement switches from target to another if there are multiple targets in the required distance
            if(lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.leftLockOntarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockOntarget;
                }
            }

            if (lockOnFlag && right_Stick_Right_Input)
            {
                right_Stick_Right_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.rightLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockOnTarget;
                }
            }
            cameraHandler.SetCameraHeight();
        }

      
    }
}

