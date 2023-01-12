using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class UIManager : MonoBehaviour
    {
       
        public PlayerInventory playerInventory;
        public EquipmmentWindowUI equipmmentWindowUI;

        [Header("UI windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject equipmentScreenWindow;
        public GameObject weaponInventoryWindow;

        [Header("Weapon Inventory")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool LefttHandSlot01Selected;
        public bool LefttHandSlot02Selected;


        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;       
        public Transform weaponInventorySlotParent;
        WeaponInventorySlot[] weaponInventorySlots;

       

        private void Start()
        {
            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
            equipmmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        }

        public void UpdateUI()
        {
            #region weapon inventory slots
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerInventory.weaponsInventory.Count)
                {
                    if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                        weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion
        }
        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }

        public void CloseAllInventoryWindows()
        {
            ResetAllSelectslot();
            weaponInventoryWindow.SetActive(false);
            equipmentScreenWindow.SetActive(false);
        }

        public void ResetAllSelectslot()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            LefttHandSlot01Selected = false;
            LefttHandSlot02Selected = false;
        }
    }
}

