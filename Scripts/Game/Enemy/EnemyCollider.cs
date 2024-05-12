using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private GameManager gManager;

    public AudioSource death;
    public GameObject particle;

    private void Start()
    {
        gManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyFix"))
        {
            particle.SetActive(true);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss"))
        {
            particle.SetActive(true);
            Destroy(gameObject);
        }


        if (collision.CompareTag("Shield"))
        {
            death.Play();
            particle.SetActive(true);
            Destroy(gameObject);
        }

       if(collision.CompareTag("Player"))
        {
            gManager.isPaused = true;
            gManager.deathSound.Play();
        }
    }
}
