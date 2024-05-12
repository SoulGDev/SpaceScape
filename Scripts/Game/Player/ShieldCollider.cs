using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    public AudioSource shieldSound;
    private GameManager gManager;
    public PlayerBuff playerBuff;

    void Start()
    {
        gManager = FindObjectOfType<GameManager>();
    }
    
    public void Sound()
    {
        shieldSound.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Sound();
            gManager.enemy ++;
        }

        if(other.CompareTag("Shield"))
        {
            playerBuff.timeShield += 5;
        }
    }
}
