using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using TMPro;

public class RecenterPlayer : MonoBehaviour
{

    [Header("Input References")]
    //This references an assigned Input and allows any actions to be performed using it.
    [SerializeField] private InputActionProperty recenterButton;

    //This just makes a gap between the values in the Editor.
    [Space(10)]

    [Header("Recenter References")]
    [SerializeField] private Transform playerHead;
    [SerializeField] private Transform originPosition;
    [SerializeField] private Transform targetPosition;




    private void Start() 
    {
        //This delays the function for a second so the player starts off in the right orientation when the game starts.
        Invoke("Recenter", .1f);
    }

    private void Update()
    {

        //This checks for input.
        if(recenterButton.action.WasPressedThisFrame())
        {
            //This calls the Recenter function.
            Recenter();
        }
    }

    private void Recenter()
    {
        //This gets the XR Origin component.
        XROrigin xROrigin = GetComponent<XROrigin>();
        //This calls a function in the XR component that moves the XR to a position in the world space.
        xROrigin.MoveCameraToWorldLocation(targetPosition.position);
        //This makes the camera point in a desired rotation and axis.
        xROrigin.MatchOriginUpCameraForward(targetPosition.up, targetPosition.forward);
    }   

}
