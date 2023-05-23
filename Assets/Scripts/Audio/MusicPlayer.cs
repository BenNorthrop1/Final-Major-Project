using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    private void Update() 
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }
}
