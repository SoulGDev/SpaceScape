using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnSpawnsE : MonoBehaviour
{
    public GameManager Manager;
    private float tempo;
    
    [BoxGroup("")] public GameObject[] gameObjects;
    [BoxGroup("")] public float[] tempos;

    bool v = true;

    void Update()
    {
        if (!Manager.isPaused)
        {
            if(v)
            {
                tempo += Time.deltaTime;
            }
            float item = tempos[tempos.Length - 1];
            for (int i = 0; i < tempos.Length; i++)
            {
                if (tempo > tempos[i])
                {
                    gameObjects[i].SetActive(true);
                }
            }
            if (tempo > item)
            {
                v = false;
            }
        }
    }
}
