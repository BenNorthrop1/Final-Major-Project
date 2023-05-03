using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class OceanRestartTrigger : MonoBehaviour
{
    [Header("Drowning Effect Volume")]
    public Volume drowningEffect;

    [Header("Drowning Effect Multipliers")]
    public float gainMultiplier = 1f;
    public float decreaseMultiplier = 1f;

    [Header("Transforms")]
    public Transform playerTransform;
    public Transform respawnPoint;

    private bool leftUnder;
    private float volumeWeight;
    
    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Player")
        {
            volumeWeight += gainMultiplier * Time.deltaTime;  
            leftUnder = false;
        }
    }
    private void OnTriggerExit(Collider other) 
    {       
        if(other.tag == "Player")
        {
            leftUnder = true;
        }    
    }

    private void Update() 
    {
        float clampedWeight = Mathf.Clamp01(volumeWeight);
        drowningEffect.weight = clampedWeight;

        if(clampedWeight == 1)
        {
            playerTransform.transform.position = respawnPoint.transform.position;
        }

        if(leftUnder == true)
        {
            volumeWeight -= decreaseMultiplier * Time.deltaTime;
        }
    }

}
