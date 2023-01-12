using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{

    public static AudioClip Death;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        Death = Resources.Load<AudioClip>("Die");
        audioSrc = GetComponent<AudioSource>();
    }
    
}
