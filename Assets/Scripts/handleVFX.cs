using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class handleVFX : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] vfx;
        public GameObject vfxClip1;
        public GameObject vfxClip2;



        //VFX manager to turn on and off according to the events you create in the animation handler
        private void Start()
        {
            vfx = GetComponentsInChildren<ParticleSystem>();
        }
        public void PlayFirstVFX()
        {
            vfx[0].Play();
           
        }
        public void StopFirstVFX()
        {
            vfx[0].Stop();
            
        }

        public void PlaySecondVFX()
        {
            vfx[1].Play();

        }
        public void StopSecondVFX()
        {
            vfx[1].Stop();

        }

        public void PlayThirdVFX()
        {
            vfx[2].Play();

        }
        public void StopThirdVFX()
        {
            vfx[2].Stop();

        }

        public void PlayFourthVFX()
        {
            vfx[3].Play();

        }
        public void StopFourthVFX()
        {
            vfx[3].Stop();

        }

        public void PlayParticles()
        {
            StartCoroutine(PlayVFXspear());
        }

        IEnumerator PlayVFXspear()
        {
            vfxClip1.SetActive(true);
            vfxClip2.SetActive(true);

            yield return new WaitForSeconds(1f);

            vfxClip1.SetActive(false);
            vfxClip2.SetActive(false);
        }
    }
}

