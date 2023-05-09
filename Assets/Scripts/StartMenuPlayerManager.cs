using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StartMenuPlayerManager : MonoBehaviour
{
    public Transform mainMenu;

    private void Start() 
    {
        transform.LookAt(mainMenu.position);
    }
}
