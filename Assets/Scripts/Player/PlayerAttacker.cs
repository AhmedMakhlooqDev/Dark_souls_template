using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        public string lastAttack;
        InputHander inputHander;
        public AudioSource firstAttack;
        public AudioSource secondAttack;
        PlayerManager playerManager;
        

        public void Awake()
        {
           
            playerManager = GetComponent<PlayerManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHander = GetComponent<InputHander>();
        }

        public void HandleLBAction()
        {
            PerformLBBlockingAction();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            // if the combo flag is triggered, play the second animation assigned after the first attack creating a combo attack
            if (inputHander.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);
                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    animatorHandler.PlayerTargetAnimation(weapon.OH_Light_Attack_3, true);
                    secondAttack.Play();
                    
                }
                               
            }
           
            

        }

        public void HandleLightWeightAttack(WeaponItem weapon)
        {

            animatorHandler.PlayerTargetAnimation(weapon.OH_Light_Attack_1, true);
            //assign last attack to not play first animation twice
            lastAttack = weapon.OH_Light_Attack_1;
            firstAttack.Play();
        }

        public void HandleHeavyWeightAttack(WeaponItem weapon)
        {
            animatorHandler.PlayerTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            lastAttack = weapon.OH_Heavy_Attack_1;
            
        }

        private void PerformLBBlockingAction()
        {
            if (playerManager.isInteracting)
                return;
            if (playerManager.isBlocking)
                return;
            animatorHandler.PlayerTargetAnimation("Block", false, true);
            playerManager.isBlocking = true;
        }
    }

}
