using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    [CreateAssetMenu(menuName = "Items/Consumables/Flask")]
    public class FlaskItem : ConsumableItem
    {
        //[Header("Flask Type")]
        //public bool estusFlask;
        //public bool ashenFlask;

        //[Header("Recovery Amount")]
        //public int healthRecoverAmount;
        //public int focusPointsRecoverAmount;

        //[Header("Recovery FX")]
        //public GameObject recoveryFX;

        //public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, WeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager)
        //{
        //    base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerEffectsManager);
        //    GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
        //    playerEffectsManager.currentParticleFX = recoveryFX;
        //    playerEffectsManager.amountToBeHealed = healthRecoverAmount;
        //    playerEffectsManager.instantiatedFXModel = flask;
        //    weaponSlotManager.rightHandSlot.UnloadWeapon();
        //}
    }
}
