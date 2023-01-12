using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class PhaseManager : MonoBehaviour
    {
        CharacterStats character;
        public GameObject bossPrefab;
        float timer = 0f;

        private void Awake()
        {
            character = GetComponent<CharacterStats>();
        }

        //assign the boss Phase 1 to boss phase 2
        //if the phase 1 boss is dead enable phase 2 boss game object
        private void Update()
        {
            if (character.isDead)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                if (timer >= 5)
                {
                    bossPrefab.SetActive(true);
                }
            }

            
        }

        
    }
}

