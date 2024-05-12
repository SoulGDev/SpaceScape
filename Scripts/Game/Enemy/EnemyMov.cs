using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{    
    [Foldout("Configurações")] public float speed = 5f;
    [Foldout("Configurações")] public float speedInicial = 5f;
    [Foldout("Configurações")] [Tooltip("Tempo que ficará ativo na cena")] public float timeMax;
    [Foldout("Configurações")] public string tagPlayer = "Player";
    [Foldout("Configurações")] public GameManager gManager;

    private Vector2 direcao;
    private Transform player;
    private float tempo;

    void Start()
    {
        player = GameObject.FindWithTag(tagPlayer)?.transform;
        direcao = player.position - transform.position;
        gManager = FindObjectOfType<GameManager>();
        speedInicial = speed;
    }

    void Update()
    {
        if(gManager.debuffAst)
        {
            speed = 2;
        }
        else
        {
            speed = speedInicial;
        }
        if (player != null)
        {
            direcao.Normalize();

            transform.Translate(direcao * speed * Time.deltaTime);
        }
        DesroyObj();
    }

    void DesroyObj()
    {
        tempo += Time.deltaTime;
        if(tempo >= timeMax)
        {
            Destroy(gameObject);
        }
    }
}
