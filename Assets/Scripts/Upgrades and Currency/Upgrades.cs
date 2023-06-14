using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [Header("Cannon References")]
    [SerializeField] private GameObject cannonObject;


    private BoatManager boatManager;

    private bool cannonsBought = false;

    private void Start() 
    {
        boatManager = GetComponentInParent<BoatManager>();

        cannonObject.SetActive(false);
        cannonsBought = false;
    }

    public void BuyCannons()
    {
        if(boatManager.currentMoney < 50 || cannonsBought == true)
        {
            return;
        }

        cannonObject.SetActive(true);
        cannonsBought = true;
        boatManager.currentMoney -= 50;
    }

    public void BuyMinorHealth()
    {
        if(boatManager.currentMoney < 50 || boatManager.currentHealth > 100)
        {
            return;
        }

        boatManager.currentHealth += 25;
        boatManager.currentMoney -= 50;
    }

    public void BuyFullHealth()
    {
        if(boatManager.currentMoney < 175 || boatManager.currentHealth > 100)
        {
            return;
        }

        boatManager.currentHealth += 100;
        boatManager.currentMoney -= 175;
    }
}
