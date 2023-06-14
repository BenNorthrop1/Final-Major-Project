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
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private GameObject optionsMenuObject;
    [SerializeField] private GameObject shopMenuObject;

    [Header("Ray Interactors")]
    [SerializeField] private GameObject rightHandRayInteractor;
    [SerializeField] private GameObject leftHandRayInteractor;

    private void Start() 
    {
        pauseMenuObject.SetActive(false);
        rightHandRayInteractor.SetActive(false);
        leftHandRayInteractor.SetActive(false);
        optionsMenuObject.SetActive(false);
        shopMenuObject.SetActive(true);
    }

    private void Update() 
    {
        if(pauseButton.action.WasPressedThisFrame())
        {
            if(pauseMenuObject.activeSelf == false)
            {
                pauseMenuObject.SetActive(true);
                rightHandRayInteractor.SetActive(true);
                leftHandRayInteractor.SetActive(true);
            }
            else
            {
                pauseMenuObject.SetActive(false);
                rightHandRayInteractor.SetActive(false);
                leftHandRayInteractor.SetActive(false);
            }
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuLevel");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        optionsMenuObject.SetActive(true);
        shopMenuObject.SetActive(false);
    }

    public void Shops()
    {
        optionsMenuObject.SetActive(false);
        shopMenuObject.SetActive(true);
    }
}
