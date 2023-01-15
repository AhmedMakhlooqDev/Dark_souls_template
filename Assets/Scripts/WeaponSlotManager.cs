using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AM
{
    public class WeaponSlotManager : MonoBehaviour
    {
        PlayerManager playerManager;
        public WeaponHolderSlot leftHandSlot;
        public WeaponHolderSlot rightHandSlot;
        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        QuickSlotsUI quickSlotsUI;
        private void Awake()
        {
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            playerManager = GetComponentInParent<PlayerManager>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftHandWeaponDamageCollider();
                quickSlotsUI.updateWeaponQuickSlotUI(true, weaponItem);
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightHandWeaponDamageCollider();
                quickSlotsUI.updateWeaponQuickSlotUI(false, weaponItem);
            }
        }

        #region Handle Weapons damage collider
        
        private void LoadLeftHandWeaponDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        private void LoadRightHandWeaponDamageCollider()
        {
           rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenDamageCollider()
        {          
                rightHandDamageCollider.EnableDamageCollider();
        }   

        public void CloseDamageCollider()
        {   
            rightHandDamageCollider.DisableDamageCollider();
        }
        
        #endregion
    }
}

