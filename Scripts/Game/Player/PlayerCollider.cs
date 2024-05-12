using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [Foldout("Tags")] public string[] tagEnemy;

    public GameManager gManager;
    public PlayerBuff pBuff;
    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < tagEnemy.Length; i++)
        {
            if (other.CompareTag(tagEnemy[i]))
            {
                gManager.GetComponent<GameManager>().isEnd = true;
            }
        }

        if(other.CompareTag("Shield"))
        {
            pBuff.GetComponent<PlayerBuff>().shieldBool = true;
            pBuff.GetComponent<PlayerBuff>().timeShield = 5;
        }
    }
}
