using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public enum BoatState
{
    AnchorDown,
    MovingForwards,
    WindAccelerate
};

[RequireComponent(typeof(Rigidbody))]
public class BoatMovement : MonoBehaviour
{
    [HideInInspector] 
    public BoatState boatState;
    [HideInInspector] public bool effectedByWind;

    public WaterSurface water;

    [Header("Speed Values")]
    public float passiveSpeed;
    public float windSpeed;
    public float maxSpeed;
    public float turnSpeed;

    private Rigidbody boatRb;
    private SteeringWheel steeringWheel;

    private float currentSpeed;

    private void Awake() 
    {
        boatRb = GetComponent<Rigidbody>();
        water = FindObjectOfType<WaterSurface>();
        steeringWheel = GetComponentInChildren<SteeringWheel>();
    }

    private void Update() 
    {
        water.transform.position =  new Vector3(transform.position.x, 0, transform.position.z);

        if(!effectedByWind)
        {
            currentSpeed += passiveSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed += windSpeed * Time.deltaTime;         
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
    }

    private void FixedUpdate() 
    {
        Vector3 forwardForce = transform.forward * currentSpeed;
        Quaternion turnRotation = Quaternion.Euler(0f, steeringWheel.currentAngle * turnSpeed, 0f);

        switch (boatState)
        {
            case BoatState.AnchorDown:

                currentSpeed--;

                if(currentSpeed <= 0)
                {
                    currentSpeed = 0;
                }

            break;

            case BoatState.MovingForwards:

            boatRb.AddForce(forwardForce, ForceMode.Force);

            boatRb.MoveRotation(boatRb.rotation * turnRotation);

            break;

        }
    }
}
