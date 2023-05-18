using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;


[RequireComponent(typeof(Rigidbody))]
public class BoatMovement : MonoBehaviour
{
    public bool effectedByWind;

    [Header("Wheel Component")]
    public HingeJoint steeringWheel;


    [Header("Speed Values")]
    public float regularSpeed;
    public float turnSpeed;
    public float maxTurnAngle = 180;

    private Rigidbody boatRb;
    private WaterSurface water;

    private void Awake() 
    {
        boatRb = GetComponent<Rigidbody>();
        water = FindObjectOfType<WaterSurface>();
    }

    private void Update() 
    {
        //This updates the Oceans position dynamically
        water.transform.position =  new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void FixedUpdate() 
    {
        //float turnAngle = Mathf.Clamp(steeringWheel.angle/ maxTurnAngle, -1 , 1);

        //transform.rotation = Quaternion.Euler(0, turnAngle, 0);

        boatRb.AddForce(Vector3.forward * regularSpeed, ForceMode.Acceleration);
    }
}

