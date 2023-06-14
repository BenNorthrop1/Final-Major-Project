using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    public float bulletLife = 5;

    [Header("Audio Effects")]
    [SerializeField] private AudioClip[] woodImpact;
    [SerializeField] private AudioClip targetDing;
    
    private Collider bulletCollider;
    private Rigidbody bulletRigidbody;
    private AudioSource audioSource;

    
    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        bulletCollider = GetComponent<Collider>();
        bulletRigidbody = GetComponent<Rigidbody>();

        bulletRigidbody.isKinematic = false;
        
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Map")
        {
            int s = Random.Range(0, woodImpact.Length);
            audioSource.clip = woodImpact[s];
            audioSource.Play();
            bulletRigidbody.isKinematic = true;
            Destroy(gameObject, 2);
        }

        if(other.tag == "Target")
        {
            audioSource.clip = targetDing;
            audioSource.Play();
            bulletRigidbody.isKinematic = true;
            Destroy(gameObject, 2);
        }
    }
}
