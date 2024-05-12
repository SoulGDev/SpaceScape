using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBuff : MonoBehaviour
{
    private PlayerBuff player;
    void Start()
    {
        player = FindObjectOfType<PlayerBuff>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.shieldBool = true;
            Destroy(gameObject);
        }
    }
}
