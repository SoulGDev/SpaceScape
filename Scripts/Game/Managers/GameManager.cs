using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Lean;

public class GameManager : MonoBehaviour
{
    #region GameObjects
    [Foldout("GameObjects")] public SpriteRenderer playerSprite;
    [Foldout("GameObjects")] public GameObject pause;
    [Foldout("GameObjects")] public GameObject player;
    [Foldout("GameObjects")] public GameObject gameOverPanel;
    [Foldout("GameObjects")] public GameObject semGranaPanelP;
    [Foldout("GameObjects")] public GameObject semGranaPanelE;
    [Foldout("GameObjects")] public GameObject semGranaPanelS;
    [Foldout("GameObjects")] public GameObject semGranaPanelJ;
    [Foldout("GameObjects")] public GameObject pSystem;
    [Foldout("GameObjects")] public GameObject joystickDynamic;
    [Foldout("GameObjects")] public GameObject joystickFixed;
    [Foldout("GameObjects")] public ParticleSystem skull;
    #endregion
    #region Listas e Contadores
    [BoxGroup("Listas")] public Sprite[] sprites;

    [BoxGroup("Contadores")] public float tempo;
    [BoxGroup("Contadores")] public int enemy;
    [BoxGroup("Contadores")] public int gen;
    [BoxGroup("Contadores")] private float tp;
    #endregion
    #region Bools
    [Foldout("Bools")] public bool isEnd;
    [Foldout("Bools")] public bool isPaused;
    [Foldout("Bools")] public bool isFristDeath;
    [Foldout("Bools")] public bool record;
    [Foldout("Bools")] public bool debuffAst;
    [Foldout("Bools")] private bool vibrateOn;
    [Foldout("Bools")] private bool hasVibrated;
    private bool p;
    #endregion
    #region Textos e Strings
    [Foldout("Textos")] public Text timeText;
    [Foldout("Textos")] public Text maxTimeText;
    [Foldout("Textos")] public Text enemyText;
    [Foldout("Textos")] public Text genText;

    [Foldout("Strings")] public string menu;
    [Foldout("Strings")] public string game;
    #endregion
    [Foldout("Sons")] public AudioSource deathSound;

    private void Awake()
    {
        int i = PlayerPrefs.GetInt("Sprite", 0);
        playerSprite.sprite = sprites[i];
    }
    void Start()
    {
        gen =  PlayerPrefs.GetInt("Gen", 0);
        maxTimeText.text = PlayerPrefs.GetFloat("Tempo").ToString("F2");
        int i = PlayerPrefs.GetInt("Idioma");
        isFristDeath = false;
        isPaused = false;
        isEnd = false;
        Sensi();
        Vibrate();
        ControllerChoise();
        LanguageChange(i);
        p = true;
    }

    void Update()
    {
        if(!isPaused)
        {
            Count();
        }
        if(isPaused && !isFristDeath)
        {
            player.GetComponent<CircleCollider2D>().enabled = false;
            pause.SetActive(true);
            int controllerChoise = PlayerPrefs.GetInt("ControllerChoise", 0);
            if (controllerChoise == 1)
            {
                joystickFixed.SetActive(false);
            }
            if (controllerChoise == 0)
            {
                joystickDynamic.SetActive(false);
            }
            if (vibrateOn && !hasVibrated)
            {
                Handheld.Vibrate();
                hasVibrated = true;
                Debug.Log("Vibrou");
            }
            if(p)
            {
                skull.Play();
                p = false;
            }
        }
        if (isPaused && isFristDeath)
        {
            GameOver();
            player.GetComponent<CircleCollider2D>().enabled = false;
            if (vibrateOn && !hasVibrated)
            {
                Handheld.Vibrate();
                hasVibrated = true;
            }
            if (p)
            {
                skull.Play();
                p = false;
            }
        }
        if(debuffAst)
        {
            tp += Time.deltaTime;
            if(tp >= 3)
            {
                tp = 0;
                debuffAst = false;
            }
        }
    }
    #region Botões
    public void GameOver()
    {
        SaveCount();
        gameOverPanel.SetActive(true);
        pause.SetActive(false);
    }

    public void BackMenu()
    {
        if (!string.IsNullOrEmpty(menu))
        {
            SceneManager.LoadScene(menu);
        }
    }

    public void Restart()
    {
        if (!string.IsNullOrEmpty(game))
        {
            SceneManager.LoadScene(game);
        }
    }

    public void Reviver(int custo)
    {
        if (gen == 0)
        {
            int idioma = PlayerPrefs.GetInt("Idioma");

            // Verifica o idioma e ativa o painel correspondente
            switch (idioma)
            {
                case 0:
                    semGranaPanelP.SetActive(true);
                    break;
                case 1:
                    semGranaPanelE.SetActive(true);
                    break;
                case 2:
                    semGranaPanelS.SetActive(true);
                    break;
                case 3:
                    semGranaPanelJ.SetActive(true);
                    break;
            }

            // Desativa os painéis após 2 segundos
            StartCoroutine(DesativarPaineisAposDelay());
        }
        else
        {
            // Restaura o jogador e configura o controle
            gen -= custo;
            isPaused = false;
            pause.SetActive(false);
            isFristDeath = true;
            p = true;
            StartCoroutine(InvencibleTime());
            int controllerChoise = PlayerPrefs.GetInt("ControllerChoise", 0);
            if (controllerChoise == 1)
            {
                joystickFixed.SetActive(true);
            }
            else
            {
                joystickDynamic.SetActive(true);
            }
        }
    }

    IEnumerator DesativarPaineisAposDelay()
    {
        // Aguarda 2 segundos
        yield return new WaitForSeconds(2f);

        // Desativa os painéis
        semGranaPanelP.SetActive(false);
        semGranaPanelE.SetActive(false);
        semGranaPanelS.SetActive(false);
        semGranaPanelJ.SetActive(false);
    }
    IEnumerator InvencibleTime()
    {
        yield return new WaitForSeconds(3f);
        player.GetComponent<CircleCollider2D>().enabled = true;
    }
    #endregion

    #region Contadores

    void Count()
    {
        tempo += Time.deltaTime;
        enemyText.text = enemy.ToString();
        timeText.text = tempo.ToString("F1");
        genText.text = gen.ToString();
        float tempoSalvo = PlayerPrefs.GetFloat("Tempo", 0f);
        if (tempo > tempoSalvo && !record)
        {
            record = true;
            pSystem.SetActive(true);
        }
    }

    #endregion

    #region Salvar
    void SaveCount()
    {
        float tempoSalvo = PlayerPrefs.GetFloat("Tempo", 0f);

        if (tempo > tempoSalvo)
        {
            PlayerPrefs.SetFloat("Tempo", tempo);
            PlayerPrefs.Save();
        }

        int ekSaved = PlayerPrefs.GetInt("EnemyKilled", 0);
        if (enemy >= ekSaved)
        {
            PlayerPrefs.SetInt("EnemyKilled", enemy);
            PlayerPrefs.Save();
        }

        PlayerPrefs.SetInt("Gen", gen);
        PlayerPrefs.Save();

    }

    #endregion

    #region Configurações

    void Vibrate()
    {
        int vibrate = PlayerPrefs.GetInt("Vibrate", 0);
        if (vibrate == 1)
        {
            vibrateOn = false;
        }
        if (vibrate == 0)
        {
            vibrateOn = true;
        }
    }

    void Sensi()
    {
        float sensi = PlayerPrefs.GetFloat("Sensibilidade", 25f);
        player.GetComponent<PlayerMovMobile>().speed = sensi;
    }

    void ControllerChoise()
    {
        int controllerChoise = PlayerPrefs.GetInt("ControllerChoise", 0);
        if (controllerChoise == 1)
        {
            joystickFixed.SetActive(true);
        }
        if (controllerChoise == 0)
        {
            joystickDynamic.SetActive(true);
        }
    }
    public void LanguageChange(int i)
    {
        if (i == 0)
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Portuguese");

        }
        if (i == 1)
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
        }
        if (i == 2)
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Spanish");
        }
        if (i == 3)
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Japanese");
        }
    }
    #endregion
}