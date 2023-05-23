using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CannonBallRemover : MonoBehaviour
{
    [Header("Cannon Ball Values")]
    [SerializeField] private float cannonBalLife = 10;
    public int cannonBallDamage = 25;
    

    [Header("Visual Effects")]  
    [SerializeField] private ParticleSystem rubbleExpolosionFX;
    [SerializeField] private ParticleSystem dirtExpolosionFX;
    
    private Collider cannonBallCollider;
    private Rigidbody cannonBallRigidbody;

    private void Awake() 
    {
        cannonBallCollider = GetComponent<Collider>();
        cannonBallRigidbody = GetComponent<Rigidbody>();

        cannonBallCollider.isTrigger = true;
        cannonBallRigidbody.isKinematic = false;
        
        Destroy(gameObject, cannonBalLife);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Rock")
        {
            print("Hit Rock");

            cannonBallRigidbody.isKinematic = true;
            dirtExpolosionFX.Play(true);
            Destroy(gameObject, 2);
        }

        if(other.tag == "Boat")
        {

            BoatManager boatManager = FindObjectOfType<BoatManager>();

            if(boatManager != null)
            {
                cannonBallRigidbody.isKinematic = true;
                boatManager.TakeDamage(cannonBallDamage);
                rubbleExpolosionFX.Play(true);
                Destroy(gameObject, 2);
            }
        }
    }
}
