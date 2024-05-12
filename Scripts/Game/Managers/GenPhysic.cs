using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenPhysic : MonoBehaviour
{
    [SerializeField] private float gravity = 9.8f; // Aceleração da gravidade

    private float verticalSpeed = 0f;
    private Transform gen;
    private float time;

    public GameManager gManager;

    void Start()
    {
        gen = transform;
        gManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        ApplyGravity(); // Aplica a gravidade ao objeto
        time += Time.deltaTime;
        if(time > 20)
        {
            Destroy(gameObject);
        }
    }

    void ApplyGravity()
    {
        if(!gManager.isEnd)
        {
            // Calcula a velocidade vertical baseada na gravidade
            verticalSpeed -= gravity * Time.deltaTime;

            // Atualiza a posição vertical do objeto
            gen.position += new Vector3(0f, verticalSpeed * Time.deltaTime, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gManager.gen++;
            Destroy(gameObject);
        }
    }
}
