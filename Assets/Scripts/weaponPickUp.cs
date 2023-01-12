using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace AM
{
    public class weaponPickUp : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            //pick up item and add to player inventory
            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;
            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();
            //play pick up animation and set movement speed to zero
            playerLocomotion.rigidbody.velocity = Vector3.zero; //Stop player from moving while picking up the item
            animatorHandler.PlayerTargetAnimation("Pick Up Item", true);
            playerInventory.weaponsInventory.Add(weapon);
            playerManager.ItemInteractableUIGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
            playerManager.ItemInteractableUIGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;

            //destroy game object once picked up 
            playerManager.ItemInteractableUIGameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}

