using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class EnemyBossManager : MonoBehaviour
    {
        public string bossName;

        UIBossHealthBar uIBossHealthBar;
        EnemyStats enemyStats;

        void Start()
        {
            uIBossHealthBar.SetBossName(bossName);
            uIBossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }

        private void Awake()
        {          
            uIBossHealthBar = FindObjectOfType<UIBossHealthBar>();
            enemyStats = GetComponent<EnemyStats>();
        }

        public void UpdateBossHealthBar(int currentHealth)
        {
            uIBossHealthBar.SetBossCurrentHealth(currentHealth);
        }

    }
}

