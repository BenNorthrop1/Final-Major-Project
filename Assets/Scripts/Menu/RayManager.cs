using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayManager : MonoBehaviour
{
    public GameObject rightRayInteractor;
    public GameObject leftRayInteractor;

    private void Awake() 
    {
        rightRayInteractor.SetActive(false);
        leftRayInteractor.SetActive(false);        
    }

}
