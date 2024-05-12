using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGen : MonoBehaviour
{
    private float percent; // valor a ser sorteado
    private float time; // contador de tempo

    [Tooltip("valor da porcentagem de chance para Spawnar")] public int valor; // valor que sera para spawnar, se menor, spawna
    [Tooltip("Valor do tempo minimo para Spawnar")]public float min; // valor do tempo para spawnar
    [Tooltip("Valor do tempo maximo para Spawnar")]public float max; // valor do tempo para spawnar

    public GameObject gen;
    public Transform playerX;
    public GameManager gManager;

    private void Start()
    {
        gManager.GetComponent<GameManager>();
    }

    void Update()
    {
        Count();
    }

    void Spawn()
    {
        float x = playerX.position.x;
        Vector3 spawn = new Vector3(x, 50, 0);
        percent = Random.Range(0, 100);
        if(percent <= valor)
        {
            Instantiate(gen, spawn, Quaternion.identity);
        }
    }

    void Count()
    {
        float spawner = Random.Range(min, max);
        if (!gManager.isPaused)
        {
            time += Time.deltaTime;
        }
        if (time >= spawner)
        {
            time = 0f;
            Spawn();
        }
    }
}
