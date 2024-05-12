using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuff : MonoBehaviour
{
    public List<GameObject> listaDePrefabs = new List<GameObject>();
    [Tooltip("Tempo no qual irá spawns o Buff")] public float spawnTime;
    [Tooltip("Tempo minimo no qual irá spawns o Buff")] public float spawnTimeMin;
    [Tooltip("Tempo maximo no qual irá spawns o Buff")] public float spawnTimeMax;
    private Camera mainCamera;
    private float time;

    public GameManager gManager;

    void Start()
    {
        mainCamera = Camera.main;
        gManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gManager.isPaused)
        {
            time += Time.deltaTime;
        }
        if (time >= spawnTime)
        {
            if (listaDePrefabs.Count > 0)
            {
                SpawnPrefab();
                spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            }

            time = 0f;
        }
    }

    void SpawnPrefab()
    {
        int indiceAleatorio = Random.Range(0, listaDePrefabs.Count);
        GameObject prefab = listaDePrefabs[indiceAleatorio];

        Vector3 spawnPosition = GetRandomViewportPosition();
        spawnPosition = mainCamera.ViewportToWorldPoint(spawnPosition);
        spawnPosition.z = 0;
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomViewportPosition()
    {
        float randomX = Random.Range(0f, 1f); 
        float randomY = Random.Range(0f, 1f);
        return new Vector3(randomX, randomY, 0f);
    }
}
