using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM
{
    public class soundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] footStepsClips;
        [SerializeField]
        private AudioClip[] DamageClips;

        private AudioSource audioSource;
        Animator anim;
        public AudioSource audio1;
        public AudioSource audio2;
        public AudioSource audio3;
        public AudioSource audio4;





        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();          
            anim = GetComponent<Animator>();


        }

        public void Steps()
        {
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
        }

        public void damage()
        {
            AudioClip clip = GetRandomDamageClip();
            audioSource.PlayOneShot(clip);
        }

        private AudioClip GetRandomClip()
        {

            return footStepsClips[UnityEngine.Random.Range(0, footStepsClips.Length)];

        }

        private AudioClip GetRandomDamageClip()
        {

            return DamageClips[UnityEngine.Random.Range(0, DamageClips.Length)];

        }

        public void playAudio1()
        {
            audio1.Play();
        }
        public void playAudio2()
        {
            audio2.Play();
        }
        public void playAudio3()
        {
            audio3.Play();
        }
        public void playAudio4()
        {
            audio4.Play();
        }
        //public void playAudio5()
        //{
        //    audio5.Play();
        //}




    }
}
