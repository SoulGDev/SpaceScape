using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Challenges : MonoBehaviour
{
    public float[] tempoParaDesbloqueio;
    public int[] inimigoParaDesbloqueio;
    public GameObject[] botoesParaDesbloqueioT;
    public GameObject[] botoesParaDesbloqueioE;

    private MenuManager mManager;

    private void Start()
    {
        OpenCustom();
        mManager = GetComponent<MenuManager>();
    }

    public void OpenCustom()
    {
        float tempoSalvo = PlayerPrefs.GetFloat("Tempo", 0f);
        int enemyKilled = PlayerPrefs.GetInt("EnemyKilled", 0);

        // Desbloquear botões com base no tempo
        for (int i = 0; i < tempoParaDesbloqueio.Length; i++)
        {
            if (tempoSalvo >= tempoParaDesbloqueio[i])
            {
                ActivateButton(botoesParaDesbloqueioT[i]);
            }
        }

        // Desbloquear botões com base no número de inimigos mortos
        for (int i = 0; i < inimigoParaDesbloqueio.Length; i++)
        {
            if (enemyKilled >= inimigoParaDesbloqueio[i])
            {
                ActivateButton(botoesParaDesbloqueioE[i]);
            }
        }
    }

    private void ActivateButton(GameObject button)
    {
        button.GetComponent<Button>().enabled = true;
        button.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        EventTrigger[] eventTriggers = button.GetComponents<EventTrigger>();
        foreach (EventTrigger trigger in eventTriggers)
        {
            trigger.enabled = false;
        }
    }
}