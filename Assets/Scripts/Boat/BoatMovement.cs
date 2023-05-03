using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;


public class BoatMovement : MonoBehaviour
{
    public WindZone globalWindZone;
    public Rigidbody boatRb;

    public float minAngle = -35f;
    public float maxAngle = 35f;


    private void Update() 
    {
        if(globalWindZone.transform.rotation == transform.rotation)
        {
            boatRb.AddForce(Vector3.forward, ForceMode.Acceleration );
        }
    }
}
