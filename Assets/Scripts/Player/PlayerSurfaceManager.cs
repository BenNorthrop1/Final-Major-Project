using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurfaceManager : MonoBehaviour
{
    public LayerMask boatMask;
   
    private void Update() 
    {
        if(Physics.SphereCast(transform.position, 0.4f, Vector3.down , out RaycastHit hit ,boatMask))
        {
            Transform boatMovement = GetComponent<BoatMovement>().transform;

            if(boatMovement != null)
            {
                transform.parent = boatMovement;
                Debug.Log("On ship");
            }
        }
        else
        {
            transform.parent = null;
            Debug.Log("Off ship");
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.4f);
    }

}
