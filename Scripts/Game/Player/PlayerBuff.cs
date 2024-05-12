using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuff : MonoBehaviour
{
    [Foldout("Bools")] public bool shieldBool;

    [Foldout("Contadores")] public float timeShield = 0;

    [BoxGroup("Textos")] public Text shieldText;

    [BoxGroup("GameObject Buffs")] public GameObject shieldGameObject;

    void Start()
    {
        
    }

    void Update()
    {
        if(shieldBool)
        {
            ShieldActive();
        }
    }

    public void ShieldActive()
    {
        timeShield -= Time.deltaTime;
        shieldGameObject.SetActive(true);
        shieldText.enabled = true;
        shieldText.text = timeShield.ToString("F1");
        if(timeShield <= 0)
        {
            shieldBool = false;
            shieldGameObject.SetActive(false);
            shieldText.enabled = false;
        }
    }
}
