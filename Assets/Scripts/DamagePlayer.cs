using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 100;
        private void OnTriggerEnter(Collider other)
        {
           PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if(playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }

        }
    }
}

