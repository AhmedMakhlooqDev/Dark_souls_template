using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AM
{
    public class UIBossHealthBar : MonoBehaviour
    {
        public TextMeshProUGUI bossName;
        Slider slider;


        private void Start()
        {
            SetUIHealthBarToInActive();
        }

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            bossName = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetBossName(string name)
        {
            bossName.text = name;
        }

        public void SetUIHealthBarToActive()
        {
            slider.gameObject.SetActive(true);
        }

        public void SetUIHealthBarToInActive()
        {
            slider.gameObject.SetActive(false);
        }

        public void SetBossMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetBossCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }
    }

}
