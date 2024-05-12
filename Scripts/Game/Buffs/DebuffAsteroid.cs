using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffAsteroid : MonoBehaviour
{
    private GameManager gManager;
    void Start()
    {
        gManager = FindAnyObjectByType<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gManager.debuffAst = true;
            Destroy(gameObject);
        }
    }
}