using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventTriggers : MonoBehaviour
{
    public Challenges challenge;
    public Text[] textoT;
    public Text[] textoE;
    private void Start()
    {
        ChangeLanguage();
    }
    public void ChangeLanguage()
    {
        int j = PlayerPrefs.GetInt("Idioma");
        for (int i = 0; i < textoT.Length; i++)
        {
            switch (j)
            {
                case 0:
                    textoT[i].text = "Permaneça " + challenge.tempoParaDesbloqueio[i] + " Segundos em uma partida para Desbloquear";
                    break;
                case 1:
                    textoT[i].text = "Stay " + challenge.tempoParaDesbloqueio[i] + " Seconds in a match to Unlock";
                    break;
                case 2:
                    textoT[i].text = "Permanecer " + challenge.tempoParaDesbloqueio[i] + " Segundos en una partida para desbloquear";
                    break;
                case 3:
                    textoT[i].text = "滞在する " + challenge.tempoParaDesbloqueio[i] + " 試合中の秒数でロックが解除されます";
                    break;
            }
            switch (j)
            {
                case 0:
                    textoE[i].text = "Destrua " + challenge.inimigoParaDesbloqueio[i] + " Inimigos em uma unica partida para Desbloquear";
                    break;
                case 1:
                    textoE[i].text = "Destroy " + challenge.inimigoParaDesbloqueio[i] + " Enemies in a single match to Unlock";
                    break;
                case 2:
                    textoE[i].text = "Destroy " + challenge.inimigoParaDesbloqueio[i] + " Enemigos en una sola partida para desbloquear";
                    break;
                case 3:
                    textoE[i].text = "破壊する " + challenge.inimigoParaDesbloqueio[i] + " シングルマッチでロックを解除できる敵";
                    break;
            }

        }
    }
}

// nao esta funcionando porque o botao esta desativado, crie uma lista de botoes e recupere as informações de challenges - chamar no start usando o for para numerar