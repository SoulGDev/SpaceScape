using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Loja : MonoBehaviour
{
    [Foldout("Lista")] public GameObject[] botoes;
    [Foldout("Lista")] public GameObject[] botoesPrincipais;
    [Foldout("Lista")] public bool[] comprado;

    public MenuManager mManager;

    [Foldout("ParticleSystem")] public ParticleSystem semGranaPanelP;
    [Foldout("ParticleSystem")] public ParticleSystem semGranaPanelE;
    [Foldout("ParticleSystem")] public ParticleSystem semGranaPanelS;
    [Foldout("ParticleSystem")] public ParticleSystem semGranaPanelJ;


    void Start()
    {
        mManager = GetComponent<MenuManager>();

        for (int i = 0; i < comprado.Length; i++)
        {
            int j = PlayerPrefs.GetInt("Comprado_" + i, -1);
            if (i == j)
            {
                comprado[i] = true;
                botoes[i].SetActive(false);
                Button button = botoesPrincipais[i].GetComponent<Button>();
                button.enabled = true;
                Image image = botoesPrincipais[i].GetComponent<Image>();
                image.color = Color.white;
            }
            else
            {
                comprado[i] = false;
                botoes[i].SetActive(true);
            }
        }
    }

    public void Comprar(int i)
    {
        int gen = PlayerPrefs.GetInt("Gen", 0);
        int valor = 5;
        if (gen >= valor)
        {
            gen -= valor;
            PlayerPrefs.SetInt("Gen", gen);
            PlayerPrefs.SetInt("Comprado_" + i, i);
            PlayerPrefs.Save();
            mManager.Time_Gen();
            Debug.Log(gen);
            Button button = botoesPrincipais[i].GetComponent<Button>();
            button.enabled = true;
            Image image = botoesPrincipais[i].GetComponent<Image>();
            image.color = Color.white;
            comprado[i] = true;
            botoes[i].SetActive(false);
        }
        else
        {
            int idioma = PlayerPrefs.GetInt("Idioma");

            // Verifica o idioma e ativa o painel correspondente
            switch (idioma)
            {
                case 0:
                    semGranaPanelP.Play();
                    break;
                case 1:
                    semGranaPanelE.Play();
                    break;
                case 2:
                    semGranaPanelS.Play();
                    break;
                case 3:
                    semGranaPanelJ.Play();
                    break;
            }

        }
    }
    public void DestroyPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
