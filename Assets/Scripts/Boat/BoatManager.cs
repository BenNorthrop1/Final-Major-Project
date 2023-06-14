using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class BoatManager : MonoBehaviour
{
    [Header("Currency")]
    public int currentMoney;

    [Header("Health Values")]
    [SerializeField] private int maxHealth = 100;

    [Header("Text Reference")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text moneyText;

    [Header("Death References")]
    [SerializeField] private GameObject deathScreenObject;

    [Header("Music Manager")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip deathSoundEffect;

    private AudioSource audioSource;
    public int currentHealth;

    private Rigidbody boatRigidbody;

    private void Awake() 
    {
        boatRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        currentMoney = 0;
        currentHealth = maxHealth;

        deathScreenObject.SetActive(false);
        boatRigidbody.isKinematic = false;
        musicAudioSource.mute = false;
    }

    private void Update() 
    {
        healthText.SetText(currentHealth.ToString());
        moneyText.SetText(currentMoney.ToString());

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(DeathSequence());
        }

        
        if(currentHealth > 100)
        {
            currentHealth = 100;
        }
    }

    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
    }

    IEnumerator DeathSequence()
    {
        boatRigidbody.isKinematic = true;
        deathScreenObject.SetActive(true);
        musicAudioSource.mute = true;
        audioSource.clip = deathSoundEffect;
        audioSource.Play();

        yield return new WaitForSeconds(8);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield break;
    }
}
