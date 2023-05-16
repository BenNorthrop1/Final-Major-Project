using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;


[RequireComponent(typeof(Rigidbody))]
public class BoatMovement : MonoBehaviour
{
    public bool effectedByWind;

    [Header("Wheel Components")]
    public HingeJoint steeringWheel;
    public Transform steeringWheelPos;
    public Transform steeringWheelPivot;

    [Header("Speed Values")]
    public float passiveSpeed;
    public float windSpeed;
    public float maxSpeed;
    public float turnSpeed;
    public float maxTurnAngle = 180;

    private Rigidbody boatRb;
    private WaterSurface water;

    private float currentSpeed;

    private void Awake() 
    {
        boatRb = GetComponent<Rigidbody>();
        water = FindObjectOfType<WaterSurface>();
    }

    private void Update() 
    {
        //This handles updating the steering Wheel Transform as the hinge joint connected body causes issues with positions

        water.transform.position =  new Vector3(transform.position.x, 0, transform.position.z);
        steeringWheel.transform.position = steeringWheelPos.transform.position;
        steeringWheel.connectedAnchor = steeringWheelPivot.position;
    }

    private void FixedUpdate() 
    {

    }
}

