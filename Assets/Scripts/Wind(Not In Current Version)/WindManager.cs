using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public WindZone globalWindZone;
    public LayerMask boatLayer;
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;

    private void Awake() 
    {
        globalWindZone = GetComponentInParent<WindZone>();
    }

    private void Update() 
    {
        HandleWindRadius();
    }

    public void HandleWindRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, boatLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            BoatMovement boatMovement = colliders[i].transform.GetComponent<BoatMovement>();

            if(boatMovement != null)
            {
                Vector3 targetDirection = boatMovement.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                {
                    //boatMovement.effectedByWind = true;
                }
                else
                {
                    //boatMovement.effectedByWind = false;
                }
            }
        }
        
    }

    private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red; //replace red with whatever color you prefer
            Vector3 fovLine1 = Quaternion.AngleAxis(maximumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Vector3 fovLine2 = Quaternion.AngleAxis(minimumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);
        }
}
