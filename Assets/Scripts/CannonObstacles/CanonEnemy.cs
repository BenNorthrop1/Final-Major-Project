using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CanonEnemy : MonoBehaviour
{
    [Header("Detection Values")]
    [SerializeField] private float detectionRadius;




    [Header("Detection Layers")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask ignoreLayers;

    [Header("Cannon References")]
    [SerializeField] private GameObject cannonObject;
    public Transform cannonShaft;
    [SerializeField] private Transform startPos;
    [SerializeField] private GameObject cannonBallPrefab;


    [Header("Cannon Values")]
    [SerializeField] private float cannonSpeed;
    [SerializeField] private float cannonCooldown;

    
    [Header("Cannon Effects")]
    [SerializeField] private ParticleSystem cannonSmoke;

    [Header("Ship Height")]
    [SerializeField] private float shipBodyHeight = 10f;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip[] cannonSounds;

    private Transform shipTransform;
    private bool canShoot = true;
    private AudioSource audioSource;


    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Physics.CheckSphere(transform.position, detectionRadius, playerLayer))
        {

            //This searches the scene for the ships transform and puts it into a value.
            shipTransform = FindObjectOfType<BoatMovement>().transform;

            Vector3 shipBodyTransform = new Vector3(shipTransform.position.x, shipBodyHeight, shipTransform.position.z);

            Debug.DrawLine(startPos.position, shipBodyTransform, Color.yellow);

            RaycastHit hit;
            if(Physics.Linecast(startPos.position, shipBodyTransform, out hit, ignoreLayers))
            {
                //blocked
            }
            else
            {
                Vector3 cannonRotY = new Vector3(shipBodyTransform.x, transform.position.y , shipBodyTransform.z);
                transform.LookAt(cannonRotY);

                cannonShaft.LookAt(shipBodyTransform, Vector3.up);

                if(canShoot == true)
                {
                    Invoke("ShootCannon", cannonCooldown);
                    canShoot = false;
                }
            }
        }
    }


    private void ShootCannon()
    {
        int s = Random.Range(0, cannonSounds.Length);
        audioSource.clip = cannonSounds[s];
        
        GameObject cannonBall = Instantiate(cannonBallPrefab, startPos.position, startPos.rotation);
        cannonBall.GetComponent<Rigidbody>().velocity = startPos.forward * cannonSpeed;
        audioSource.Play();
        canShoot = true;
    }



    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
