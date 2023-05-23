using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

//This makes it so whatever object you put this script on requires a rigidbody to function.
[RequireComponent(typeof(Rigidbody))]
public class BoatMovement : MonoBehaviour
{
    [Header("Speed Values")]
    [SerializeField] private float moveSpeed = 35f;
    [SerializeField] private float turnSpeed = 35f;

    //This just makes a gap between the values in the Editor.
    [Space(10)]

    [Header("Input References")]
    [SerializeField] private InputActionProperty moveInput;

    [Space(5)]

    [SerializeField] private InputActionProperty turnInput;

    [Header("Steering Wheel Visuals")]
    [SerializeField] private Transform wheelTransform;

    private Vector2 moveDirection;
    private float turnDirection;

    private Rigidbody boatRigidbody;
    private WaterSurface oceanObject;
    

    private void Awake() 
    {
        boatRigidbody = GetComponent<Rigidbody>();
        //This finds the Water component in the scene, which usually wouldn't be too efficent but as there should only be one per scene it works fine.
        oceanObject = FindObjectOfType<WaterSurface>();
    }
    

    private void Update() 
    {
        //This updates the Oceans position dynamically.
        oceanObject.transform.position =  new Vector3(transform.position.x, 0, transform.position.z);
        
        //This updates the input every frame.
        //I put the Input in the Update as its more consistently updating every frame rather then multiple.
        moveDirection = moveInput.action.ReadValue<Vector2>();
        turnDirection = turnInput.action.ReadValue<Vector2>().x;

        #region WheelAnimation

        if(turnDirection >= .1)
        {
            wheelTransform.Rotate(0, 0, -10 * Time.deltaTime);
        }
        else if (turnDirection <= -.1)
        {
            wheelTransform.Rotate(0, 0, 10 * Time.deltaTime);
        }
        else if(turnDirection == 0)
        {
            wheelTransform.Rotate(0, 0, 0);
        }

        #endregion
    }

    private void FixedUpdate() 
    {
        //I'm using Fixed Update for the Rigidbody as it returns a better result and is recommended by Unity.
        //Fixed update runs per Physics tick which is less frequent then update.


        #region BoatMovement

        //This stores the rotation in which the object is pointing.
        Quaternion yaw = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        //This multiplies the yaw by the input, which returns the direction in which we move.
        Vector3 direction = yaw * new Vector3(0, 0, moveDirection.y);
        
        //This moves the Rigidbody towards a position in this case the direction we defined previously every frame multiplied by a value I can configure.
        boatRigidbody.MovePosition(boatRigidbody.position + direction * Time.fixedDeltaTime * moveSpeed);

        #endregion
        
        #region BoatRotation

        //This defines the axis we rotate on.
        Vector3 axis = Vector3.up;
        //This returns an angle which rotates along the Y axis using the value above multiplied by Every physics frame and user input.
        float angle = turnSpeed * Time.fixedDeltaTime * turnDirection;

        //This stores a rotation which uses the previously acquired axis and angle to find the turn position and rotation.
        Quaternion rotateAmount = Quaternion.AngleAxis(angle, axis);

        //This rotates the rigidbody by the rotation amount.
        boatRigidbody.MoveRotation(boatRigidbody.rotation * rotateAmount);

        #endregion
    }
}

