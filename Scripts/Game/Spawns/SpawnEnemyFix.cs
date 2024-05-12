using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyFix : MonoBehaviour
{
    public ParticleSystem pSystem;
    public List<GameObject> listaDePrefabs = new List<GameObject>();
    [Tooltip("Tempo minimo no qual irá spawnar o inimigo")] public float spawnTimeMin;
    [Tooltip("Tempo maximo no qual irá spawnar o inimigo")] public float spawnTimeMax;
    private Camera mainCamera;
    private float time;
    
    public GameManager gManager;

    private float spawnTime;

    void Start()
    {
        mainCamera = Camera.main;
        gManager.GetComponent<GameManager>();
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
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
                StartCoroutine(SpawnWithDelay(2f)); // Chama o Spawn com um atraso de 2 segundos
            }

            time = 0f;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        }
    }

    IEnumerator SpawnWithDelay(float delay)
    {
        // Aguarda o atraso antes de tocar o Particle System
        //yield return new WaitForSeconds(delay);

        Vector3 spawnPosition = GetRandomViewportPosition();
        spawnPosition = mainCamera.ViewportToWorldPoint(spawnPosition);
        spawnPosition.z = 0;

        Particle(spawnPosition); // Passa a posição do spawn como argumento para Particle

        // Aguarda mais 1 segundo antes de chamar o Instantiate
        yield return new WaitForSeconds(2f);
        SpawnPrefab(spawnPosition); // Passa a posição do spawn como argumento para SpawnPrefab
    }

    void SpawnPrefab(Vector3 spawnPosition)
    {
        int indiceAleatorio = Random.Range(0, listaDePrefabs.Count);
        GameObject prefab = listaDePrefabs[indiceAleatorio];

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomViewportPosition()
    {
        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);
        return new Vector3(randomX, randomY, 0f);
    }

    void Particle(Vector3 spawnPosition)
    {
        pSystem.transform.position = spawnPosition;
        pSystem.Play();
    }
}
