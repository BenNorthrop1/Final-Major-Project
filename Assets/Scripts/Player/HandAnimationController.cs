using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    [Header("Input References")]
    [SerializeField] private InputActionProperty pinchAnimationAction;

    //This just makes a gap between the values in the Editor.
    [Space(10)]

    [SerializeField] private InputActionProperty gripAnimationAction;

    private Animator handAnimator;

    private void Awake() 
    {
        handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {   
        //Reads the input on the trigger into a float.
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        //Uses the trigger value to control the hands blend tree.
        handAnimator.SetFloat("Trigger", triggerValue);

        //Reads the input on the grip into a float.
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        //Uses the grip value to control the hands blend tree.
        handAnimator.SetFloat("Grip", gripValue);
    }
}
