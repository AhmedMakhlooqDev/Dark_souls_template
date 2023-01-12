﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AM{
    public class UIEnemyHealthBar : MonoBehaviour
    {
        Slider slider;
        float timeUntilBarIsHidden = 0;
        
        //assign enemy health level value to the UI health bar
        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
        }

        public void SetHealth(int health)
        {
            slider.value = health;
            timeUntilBarIsHidden = 3;
        }

        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        private void Update()
        {
            timeUntilBarIsHidden = timeUntilBarIsHidden - Time.deltaTime;

            if (slider != null)
            {
                if (timeUntilBarIsHidden <= 0)
                {
                    timeUntilBarIsHidden = 0;
                    slider.gameObject.SetActive(false);
                }
                else
                {
                    if (!slider.gameObject.activeInHierarchy)
                    {
                        slider.gameObject.SetActive(true);
                    }
                }
                if (slider.value <= 0)
                {
                    Destroy(slider.gameObject);
                }
            }                       
        }
    }
}

