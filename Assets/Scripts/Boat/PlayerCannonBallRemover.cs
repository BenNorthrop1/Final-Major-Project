using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerCannonBallRemover : MonoBehaviour
{
    [Header("Cannon Ball Values")]
    [SerializeField] private float cannonBalLife = 10;
    public int cannonBallDamage = 25;
    

    
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
            cannonBallRigidbody.isKinematic = true;
            Destroy(gameObject, 2);
        }

        if(other.CompareTag("Cannons"))
        {
            CannonHealth cannonHealth = other.GetComponent<CannonHealth>();

            if(cannonHealth != null)
            {
                cannonBallRigidbody.isKinematic = true;

                cannonHealth.TakeDamage(cannonBallDamage);

                Destroy(gameObject, 2);
            }

        }
    }
}
