using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AM
{


    public class PlayerManager : CharachterManager
    {
        InputHander inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        public bool isInteracting;
        AnimatorHandler animatorHandler;
        InteractableUI interactableUI;
        public GameObject InteractableUIGameObject;
        public GameObject ItemInteractableUIGameObject;

        [Header("Player Flags")]
        public bool isSprinting; 
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;
        public bool isInvunerable;
        public bool isBlocking;
        
        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            inputHandler = GetComponent<InputHander>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            interactableUI = FindObjectOfType<InteractableUI>();
        }

        // Start is called before the first frame update
       

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");          
            canDoCombo = anim.GetBool("canDoCombo");
            isInvunerable = anim.GetBool("isInvunerable");
            animatorHandler.canRotate = anim.GetBool("canRotate");
            anim.SetBool("isBlocking", isBlocking);
            anim.SetBool("isInAir", isInAir);
            isUsingRightHand = anim.GetBool("isUsingRightHand");
            isUsingLeftHand = anim.GetBool("isUsingLeftHand");
            inputHandler.TickInput(delta);

            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleJumping();

            CheckForInteractable();


        }
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            playerLocomotion.HandleMovement(delta);            
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleRotation(delta);

        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;           
            inputHandler.rb_input = false;
            inputHandler.rt_input = false;
            inputHandler.d_pad_Up = false;
            inputHandler.d_pad_Down = false;
            inputHandler.d_pad_Right = false;
            inputHandler.d_pad_Left = false;
            inputHandler.a_input = false;
            inputHandler.inventory_input = false;
            inputHandler.jump_input = false;

            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

            // apply in Air timer
            if (isInAir)
            {
                playerLocomotion.inAirTimer += Time.deltaTime;
            }
        }

        public void CheckForInteractable()
        {
            RaycastHit hit;

            if(Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit,  1f, cameraHandler.ignoreLayers))
            {
                //if the item is tagged as interactable and the player has collided with it show the pop up interaction text 
                if(hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if(interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        InteractableUIGameObject.SetActive(true);

                        if (inputHandler.a_input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                // when the Item is picked up show the item pop up which includes the item information
                if(InteractableUIGameObject != null)
                {
                    InteractableUIGameObject.SetActive(false);
                }

                if(ItemInteractableUIGameObject != null && inputHandler.a_input)
                {
                    ItemInteractableUIGameObject.SetActive(false);
                }
            }
        }

    }
}