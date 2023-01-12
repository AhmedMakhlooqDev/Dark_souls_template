using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.6f;
        public string interactableText;

        private void OnDrawGizmosSelected()
        {
            //create sphere gizmo to display the UI pop up
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void Interact(PlayerManager playerManager)
        {
            //called when player interacts
            Debug.Log("touch");
        }
    }
}

