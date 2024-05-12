using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> listaDePrefabs = new List<GameObject>();
    public float spawn = 2; 

    private GameManager gManager;
    float tempo;

    void Start()
    {
        gManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (!gManager.isPaused)
        {
            tempo += Time.deltaTime;
            if(tempo >= spawn)
            {
                SpawnPrefab();
                tempo = 0;                
                if(spawn != 0.4f)
                {
                    spawn -= 0.01f;
                }
            }
            //Dificulty();
        }
    }

    void SpawnPrefab()
    {
        float randomX, randomY;
        do
        {
            randomX = Random.Range(-50, 50);
            randomY = Random.Range(-50, 50);
        } while ((randomX >= -30 && randomX <= 30) || (randomY >= -30 && randomY <= 30));

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        int randomIndex = Random.Range(0, listaDePrefabs.Count);
        GameObject prefab = listaDePrefabs[randomIndex];
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
    //void Dificulty()
    //{
    //    float elapsedTime = Time.timeSinceLevelLoad;
    //    if (elapsedTime <= 60f)
    //    {
    //        maxEnemy = Mathf.FloorToInt(elapsedTime / 1f);
    //    }
    //    else if (elapsedTime <= 180f)
    //    {
    //        maxEnemy = Mathf.FloorToInt((elapsedTime - 60f) / 5f) + 6;
    //    }
    //    else
    //    {
    //        maxEnemy = Mathf.Min(Mathf.FloorToInt((elapsedTime - 180f) / 10f) + 11, 60); // Limite máximo de 30 inimigos
    //    }
    //}
}
