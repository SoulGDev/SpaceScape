using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLife : MonoBehaviour
{
    [SerializeField] private GameManager gManager;
    [SerializeField] private GameObject extraLife;
    [SerializeField] private Camera mainCamera;
    [SerializeField] [Tooltip("Tempo para spawnar a vida Extra")] private float spawn;

    private float time;
    private void Start()
    {
        gManager = FindObjectOfType<GameManager>();
        mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if(gManager.isFristDeath)
        {
            time += Time.deltaTime;
            if(time > spawn)
            {
                SpawnPrefab();
                gManager.isFristDeath = false;
                time = 0;
            }
        }
    }

    void SpawnPrefab()
    {
        Vector3 spawnPosition = GetRandomViewportPosition();
        spawnPosition = mainCamera.ViewportToWorldPoint(spawnPosition);
        spawnPosition.z = 0;
        Instantiate(extraLife, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomViewportPosition()
    {
        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);
        return new Vector3(randomX, randomY, 0f);
    }

}
