using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFixed : MonoBehaviour
{
    private Transform tr;
    private float time;
    private bool s;
    private GameManager gManager;
    void Start()
    {
        tr = GetComponent<Transform>();
        s = true;
        tr.localScale = new Vector3(0, 0, 0);
        gManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
       if(s)
        {
            time += Time.deltaTime;
        }
        float x = time;
        float y = time;
        tr.localScale = new Vector3(x, y, 0);
        if(time >= 3)
        {
            s = false;
            time -= Time.deltaTime;
            if(time  < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Player"))
        {
            gManager.isPaused = true;
        }
    }
}
