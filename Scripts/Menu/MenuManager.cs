using NaughtyAttributes;
using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Lean;

public class MenuManager : MonoBehaviour
{
    [BoxGroup("Listas")] public Sprite[] sprites;
    [BoxGroup("Listas")] public Image[] botoes;


    [Header("GameObjects importantes")]
    [Tooltip("Sprite da Imagem Selecionada para o Player, aparecerá no Menu.")]
    [Foldout("GameObjects")][SerializeField] private Image imagePlayer;
    [Foldout("GameObjects")][SerializeField] private Text genText;
    [Foldout("GameObjects")][SerializeField] private Text timeText;
    [Foldout("GameObjects")][SerializeField] private Text controllerText;
    [Foldout("GameObjects")][SerializeField] private Toggle toggle;
    [Foldout("GameObjects")][SerializeField] private Slider sensi;
    [Foldout("GameObjects")][SerializeField] private GameObject controleFixo;
    [Foldout("GameObjects")][SerializeField] private GameObject controleDinamico;
    [Foldout("GameObjects")][SerializeField] private Dropdown dropdown;

    private int stateV;
    private int stateC;


    private void Awake()
    {
        GetSaveNUmb();
    }
    void Start()
    {
        Time_Gen();
        stateV = PlayerPrefs.GetInt("Vibrate", 0);
        sensi.value = PlayerPrefs.GetFloat("Sensibilidade", 25);
        toggle.isOn = (stateV == 0);
        int ct = PlayerPrefs.GetInt("ControllerChoise", 0);
        if (ct == 0)
        {
            //controllerText.text = "Dinamico";
            controleDinamico.SetActive(true);
            controleFixo.SetActive(false);
        }
        else
        {
            //controllerText.text = "Fixo";
            controleDinamico.SetActive(false);
            controleFixo.SetActive(true);
        }
        int i = PlayerPrefs.GetInt("Idioma");
        dropdown.value = i;
        LanguageChange(i);
    }

    void Update()
    {
        
    }

    public void Time_Gen()
    {
        int gen = PlayerPrefs.GetInt("Gen", 0);
        genText.text = gen.ToString();
        float time = PlayerPrefs.GetFloat("Tempo", 0);
        timeText.text = time.ToString("F2");
    } // atualiza os valores de tempo e gema

    public void LoadScene(string cena)
    {
        SceneManager.LoadScene(cena);
    } // carrega a cena do game

    public void SaveSpriteIndex(int index)
    {
        PlayerPrefs.SetInt("Sprite", index);
    } // salva o numero que será usado para mudar o sprite do player

    public void GetSaveNUmb()
    {
        int i = PlayerPrefs.GetInt("Sprite", 0);
        imagePlayer.GetComponent<Image>().sprite = sprites[i];
    } // usado para mudar o sprite do player no menu


    #region Configurações

    public void SensiChange()
    {
        float sensibilidade = sensi.value;
        PlayerPrefs.SetFloat("Sensibilidade", sensibilidade);
    }

    public void VibrateChange()
    {
        stateV = (stateV == 0) ? 1 : 0;
        PlayerPrefs.SetInt("Vibrate", stateV);
    }

    public void ControllerChoise()
    {
        stateC = (stateC == 0) ? 1 : 0;
        PlayerPrefs.SetInt("ControllerChoise", stateC);
        if (stateC == 0)
        {
            //controllerText.text = "Dinamico";
            controleDinamico.SetActive(true);
            controleFixo.SetActive(false);
        }
        else
        {
            //controllerText.text = "Fixo";
            controleFixo.SetActive(true);
            controleDinamico.SetActive(false);
        }
    }

    public void LanguageChange(int i)
    {
        if(i == 0)
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

        PlayerPrefs.SetInt("Idioma", i);

    }

    #endregion


    #region Customização

    public void ChangeSprite()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            botoes[i].GetComponent<Image>().sprite = sprites[i];
        }
       
    } // muda o sprite do botão logo no inicio / tem que chamar

    #endregion
}
