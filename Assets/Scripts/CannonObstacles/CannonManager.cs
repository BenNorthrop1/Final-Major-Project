using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CannonManager : MonoBehaviour
{
    [Header("Cannon References")]
    [SerializeField] private Transform[] cannonSpawnPoints;
    [SerializeField] private GameObject cannonPrefab;

    [Header("Spawn Options")]
    [SerializeField] private float spawnRate;
    [SerializeField] private float cannonSpawnRadius;

    [Header("Player LayerMask")]
    [SerializeField] private LayerMask shipLayer;

    [Header("Text References")]
    [SerializeField] private TMP_Text[] enemyAmountText;

    private int maxCannonsInScene;
    private int currentCannons = 0;

    void Start()
    {
        StartCoroutine(SpawnCannons());
    }

    private void Update() 
    {
        for (int i = 0; i < enemyAmountText.Length; i++)
        {
            enemyAmountText[i].SetText(currentCannons.ToString());      
        }
    }

    IEnumerator SpawnCannons()
    {
        maxCannonsInScene = cannonSpawnPoints.Length;
                
        while(currentCannons < maxCannonsInScene)
        {
            for (int i = 0; i < cannonSpawnPoints.Length; i++)
            {
                if(currentCannons >= maxCannonsInScene)
                    yield break;


                    GameObject cannonEnemy = Instantiate(cannonPrefab, cannonSpawnPoints[i].position, Quaternion.identity);
                    cannonEnemy.name = "CannonEnemy";
                    cannonEnemy.transform.parent = cannonSpawnPoints[i].transform;
                    currentCannons++;

                yield return new WaitForSeconds(spawnRate);
            }
        }
    }


}
