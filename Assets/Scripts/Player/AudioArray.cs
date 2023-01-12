using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace AM
{
    public class AudioArray : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] clips;
        private AudioSource audioSource;
        Animator anim;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            anim = GetComponent<Animator>();

        }

        public void Step()
        {
           if(anim.GetBool("isInteracting"))
            {
               
            }
            else
            {
                AudioClip clip = GetRandomClip();
                audioSource.PlayOneShot(clip);
            }
                
        }

       

        private AudioClip GetRandomClip()
        {

            return clips[UnityEngine.Random.Range(0, clips.Length)];

        }
    }
}
