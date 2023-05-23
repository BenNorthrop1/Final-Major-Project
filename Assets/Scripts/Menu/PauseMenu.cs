using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Input")]
    [SerializeField] private InputActionProperty pauseButton;

    [Header("UI elements")]
    [SerializeField] private GameObject PauseMenuObject;

    [Header("Ray Interactors")]
    [SerializeField] private GameObject rightHandRayInteractor;
    [SerializeField] private GameObject leftHandRayInteractor;

    private void Start() 
    {
        PauseMenuObject.SetActive(false);
        rightHandRayInteractor.SetActive(false);
        leftHandRayInteractor.SetActive(false);
    }

    private void Update() 
    {
        if(pauseButton.action.WasPressedThisFrame())
        {
            if(PauseMenuObject.activeSelf == false)
            {
                PauseMenuObject.SetActive(true);
                rightHandRayInteractor.SetActive(true);
                leftHandRayInteractor.SetActive(true);
            }
            else
            {
                PauseMenuObject.SetActive(false);
                rightHandRayInteractor.SetActive(true);
                leftHandRayInteractor.SetActive(true);
            }
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
