using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class Trap : MonoBehaviour
    {

        public Animation anim;
        private void Start()
        {
            anim = GetComponent<Animation>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            anim.Play("Trap");
        }
    }
}

