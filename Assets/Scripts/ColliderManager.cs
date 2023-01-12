using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AM
{
    public class ColliderManager : MonoBehaviour
    {
        // Start is called before the first frame update

        public Collider collider;
        public Collider collider2;

        //enable and disable colliders on the animation events

        public void enableCollider()
        {
            
                collider.enabled =  true;
            
        }

        public void disableCollider()
        {

            collider.enabled = false;

        }

        //public void enableCollider2()
        //{

        //    collider2.enabled = true;

        //}

        //public void disableCollider2()
        //{

        //    collider2.enabled = false;

        //}



    }
}

