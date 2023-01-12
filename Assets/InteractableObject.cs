using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace AM
{
    public class InteractableObject : Interactable
    {

        public string levelName;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            //pick up item and add to player inventory
            Proceed(playerManager);
        }

        

        private void Proceed(PlayerManager playerManager)
        {
            InputHander inputHandler;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;
            //play pick up animation and set movement speed to zero
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();
            inputHandler = playerManager.GetComponent<InputHander>();

            playerLocomotion.rigidbody.velocity = Vector3.zero; //Stop player from moving while picking up the item
            animatorHandler.PlayerTargetAnimation("Pick Up Item", true);

            if (inputHandler.a_input)
            {
                SceneManager.LoadScene(levelName);
            }

        }

       


    }
}

