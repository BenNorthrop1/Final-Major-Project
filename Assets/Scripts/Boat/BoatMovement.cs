using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;


public class BoatMovement : MonoBehaviour
{
    public WindZone globalWindZone;
    public Rigidbody boatRb;

    public enum State
    {
        AnchorDown,
        MovingForwards,
        WindAccelerate
    };
    public State state;


    private void Start() {
        state = State.MovingForwards;
    }

    private void Update() 
    {
        switch (state)
        {
            case State.AnchorDown:
            boatRb.velocity = Vector3.zero;
                break;
            case State.MovingForwards:
            boatRb.AddForce(Vector3.forward * 2 * Time.deltaTime, ForceMode.Force);
                break;
            case State.WindAccelerate:
                break;
        }
    }
}
