using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SteeringWheel : XRBaseInteractable
{
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private float maxRotationAngle = 45.0f;

    public UnityEvent<float> OnWheelRotated;
    public float rotationSpeed;

    public float currentAngle = 0.0f;
    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isTurningLeft = false;
        isTurningRight = false;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && isSelected)
        {
            float totalAngle = FindWheelAngle();
            float angleDifference = totalAngle - currentAngle;

            if (angleDifference > maxRotationAngle && !isTurningRight)
            {
                isTurningLeft = false;
                isTurningRight = true;
            }
            else if (angleDifference < -maxRotationAngle && !isTurningLeft)
            {
                isTurningRight = false;
                isTurningLeft = true;
            }
            else if (Mathf.Abs(angleDifference) < maxRotationAngle)
            {
                isTurningLeft = false;
                isTurningRight = false;
            }

            if (isTurningLeft)
            {
                currentAngle -= Time.deltaTime * rotationSpeed;
                currentAngle = Mathf.Clamp(currentAngle, totalAngle - maxRotationAngle, totalAngle);
            }
            else if (isTurningRight)
            {
                currentAngle += Time.deltaTime * rotationSpeed;
                currentAngle = Mathf.Clamp(currentAngle, totalAngle, totalAngle + maxRotationAngle);
            }

            Quaternion targetRotation = Quaternion.Euler(0, 0, currentAngle);
            wheelTransform.rotation = Quaternion.Lerp(wheelTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            OnWheelRotated?.Invoke(currentAngle);
        }
    }

    private float FindWheelAngle()
    {
        float totalAngle = 0f;
        foreach (IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
        }
        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        return transform.InverseTransformDirection(position - wheelTransform.position).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        return Vector2.SignedAngle(Vector2.up, direction);
    }

    private float FindRotationSensitivity()
    {
        return 1.0f / interactorsSelecting.Count;
    }
}
