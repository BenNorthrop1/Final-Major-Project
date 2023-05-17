using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipState
{
    idle,
    chasing,
    attacking
};

public class EnemyShip : MonoBehaviour
{
    [Header("References")]
    public Rigidbody shipRb;
    public Transform playerShip;

    [Header("Configure Values")]
    public float distanceForChase;
    public float distanceForAttack;

    public ShipState shipState;

    private float distanceFromPlayer;

    private void Start() 
    {
        StartCoroutine(ShipBrain());
    }

    private void Update() 
    {
        distanceFromPlayer = Vector3.Distance(playerShip.position, transform.position);
    }

    IEnumerator ShipBrain()
    {
        while(true)
        {
            switch (shipState)
            {
                case ShipState.idle:
                    shipRb.velocity = Vector3.zero;

                    if(distanceFromPlayer <= distanceForChase)
                    {
                        shipState = ShipState.chasing;
                    }

                    break;
                case ShipState.chasing:
                    if(distanceFromPlayer <= distanceForAttack)
                    {
                        Vector3 right = transform.TransformDirection(Vector3.right);
                        Vector3 playerPos = playerShip.position - transform.position;

                        if(Vector3.Dot(right, playerPos) < 0)
                        {
                            print("The player is right of me");
                        }
                        else
                        {
                            
                            print("The player is left");
                        }
                    }
                    break;
                case ShipState.attacking:
                    break;
            }
        }
    }
}
