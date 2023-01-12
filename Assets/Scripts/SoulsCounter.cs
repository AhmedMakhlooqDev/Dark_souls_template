using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AM
{
    public class SoulsCounter : MonoBehaviour
    {
        public Text soulsCountText;
        

        public void setSoulCount(int soulCountNumber)
        {
            soulsCountText.text = soulCountNumber.ToString();
        }
    }

}

