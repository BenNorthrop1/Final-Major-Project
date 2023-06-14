using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class Pistol : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject ammoBullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Fire Values")]
    [SerializeField] private float fireSpeed = 20f;

    [Header("Sound Values")]
    [SerializeField] private AudioClip[] firingSounds;

    [Header("Effect Values")]
    [SerializeField] private ParticleSystem gunFX;

    private AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();        
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(Shoot);
    }

    public void Shoot(ActivateEventArgs args)
    {
        int s = Random.Range(0, firingSounds.Length);
        audioSource.clip = firingSounds[s];
        audioSource.Play(); 

        gunFX.Play(true);

        GameObject cannonBall = Instantiate(ammoBullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        cannonBall.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * fireSpeed;
    }
}
