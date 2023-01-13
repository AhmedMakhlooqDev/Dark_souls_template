using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class CharachterManager : MonoBehaviour
    {
        [Header("LockOn Attributes")]
        public Transform lockOnTransform;

        [Header("Movement flags")]
        public bool isRotatingWithRootMotion;
        public bool canRotate;

    }

}
