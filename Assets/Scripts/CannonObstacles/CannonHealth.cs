using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHealth : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int deathMoney = 25;

    [Header("Death Values")]
    [SerializeField] private float deathRadius;
    [SerializeField] private float deathSphereHeight;

    [Header("Detection Layers")]
    [SerializeField] private LayerMask playerLayer;
    public int currentHealth;
    private Vector3 deathSpherePos;
    private BoatManager boatManager;
    private void Start() 
    {
        boatManager = FindObjectOfType<BoatManager>();
        currentHealth = maxHealth;
    }

    private void Update() 
    {
        
        deathSpherePos = new Vector3(transform.position.x, deathSphereHeight, transform.position.z);

        if(Physics.CheckSphere(deathSpherePos, deathRadius, playerLayer))
        {            
            TakeDamage(50);
        }

        if(currentHealth <= 0)
        {
            boatManager.currentMoney += deathMoney;
            Destroy(this.gameObject);
        }

    }

    public void TakeDamage(int Damage)
    {
        Debug.Log("Damage Taken");
        currentHealth -= Damage;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(deathSpherePos, deathRadius);
    }
}
