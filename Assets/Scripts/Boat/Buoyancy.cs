using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Buoyancy : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Rigidbody boatRigidbody;
    [SerializeField] private WaterSurface oceanObject;

    //This just makes a gap between the values in the Editor.    
    [Space(10)]

    [Header("Buoyancy Configurable Values")]
    [SerializeField] private float submergeDepth = 1;
    [SerializeField] private float displacementAmount = 2; 
    [SerializeField] private float waterDrag = 2;
    [SerializeField] private float waterAngularDrag = 2;
    
    [Space(5)]

    [Header("Buoyancy Points")]
    [SerializeField] private int buoyancyPoints = 1;

    //This is gets the water objects information 
    private WaterSearchParameters searchParam;
    private WaterSearchResult searchResult;

    private void Awake() 
    {
        oceanObject = FindObjectOfType<WaterSurface>();
    }

    private void FixedUpdate() 
    {
        //This takes into account the scenes gravity and distribution of buoyancy points to add a constant velocity at the buoyancy points position.
        boatRigidbody.AddForceAtPosition(Physics.gravity / buoyancyPoints, transform.position, ForceMode.Acceleration);

        //This gets the waters height at the point of contact.
        searchParam.startPosition = transform.position;

        //This finds the water's surface height and puts it into a value.
        oceanObject.FindWaterSurfaceHeight(searchParam, out searchResult);


        //this checks if the buoyancy point is below the sea level
        if(transform.position.y < searchResult.height)
        {
            //This calculates the displacement multiplier by getting the sumberge depth and multiplying it by the amount
            float displacementMultiplier = Mathf.Clamp01(searchResult.height - transform.position.y / submergeDepth) * displacementAmount;

            //this controls the movement behaviour of the buoyancy points when submerged below the water, it will try to push the objects back up the surface
            boatRigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            boatRigidbody.AddForce(displacementMultiplier * -boatRigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            boatRigidbody.AddTorque(displacementMultiplier * -boatRigidbody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
