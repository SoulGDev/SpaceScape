using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] private GameManager gManager;
    private Transform scala;
    private float time;
    private float x = 2;
    private float y = 2;
    private bool s;
    void Start()
    {
        gManager = GetComponent<GameManager>();
        scala = GetComponent<Transform>();
        s = true;
    }

    private void Update()
    {
        if(s)
        {
            time += Time.deltaTime;
        }
        if (time > 8)
        {
            s = false;
            x -= Time.deltaTime * 2;
            y -= Time.deltaTime * 2;
            scala.localScale = new Vector3(x, y, 0);
        }
        if(x <= 0)
        {
            x = 0;
            y = 0;
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gManager.isFristDeath = false;
            Destroy(gameObject);
        }
    }
}
