using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCannon : MonoBehaviour
{
    public LayerMask ignoreLayers;
    public LayerMask cannonLayer;

    public float detectionRadius = 20;
    public float cannonBodyHeight = 5;



    public float cannonCooldown;
    public float cannonSpeed;

    public AudioClip[] cannonSounds;

    public AudioSource audioSource;
    public Transform cannonShaft;
    public GameObject cannonBallPrefab;
    public Transform startPos;
    private Transform cannonTransform;

    private GameObject[] cannonTransforms;
    private bool canShoot = true;


    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(transform.position, detectionRadius, cannonLayer))
        {
            cannonTransforms = GameObject.FindGameObjectsWithTag("Cannons");
            cannonTransform = FindClosestEnemy().transform;

            if(cannonTransform != null)
            {

                Vector3 cannnonBodyTransform = new Vector3(cannonTransform.transform.position.x, cannonBodyHeight, cannonTransform.position.z);

                Debug.DrawLine(startPos.position, cannnonBodyTransform, Color.yellow);

                RaycastHit hit;
                if(Physics.Linecast(startPos.position, cannnonBodyTransform, out hit, ignoreLayers))
                {
                    //blocked
                }
                else
                {

                    Vector3 cannonRotY = new Vector3(cannnonBodyTransform.x, transform.position.y , cannnonBodyTransform.z);
                    transform.LookAt(cannonRotY);

                    cannonShaft.LookAt(cannnonBodyTransform, Vector3.up);

                    if(canShoot == true)
                    {
                        Invoke("ShootCannon", cannonCooldown);
                        canShoot = false;
                    }
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
    
    public GameObject FindClosestEnemy()
    {
        GameObject ClosestCannon = gameObject;
        float closestDist = Mathf.Infinity;

        foreach(GameObject cannon in cannonTransforms)
        {

            if(cannon != gameObject)
            {
                float cannonDist = Vector3.Distance(transform.position, cannon.transform.position);

                if(cannonDist < closestDist)
                {
                    closestDist = cannonDist;
                    ClosestCannon = cannon;
                }
            }
        }

        return ClosestCannon;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
