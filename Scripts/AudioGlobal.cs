using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioGlobal : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioListen;
    public AudioSource audioDeath;
    public AudioSource audioShield;

    void Start()
    {
        GetVolume();
    }

    public void SetVolume(float volume)
    {
        //volume = Mathf.Clamp01(volume);
        volume = volumeSlider.value; //audioListen.volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
    private void Update()
    {
        if(volumeSlider != null)
        {
            audioListen.volume = volumeSlider.value;
        }
    }

    public void GetVolume()
    {
        float volumeG = PlayerPrefs.GetFloat("Volume", 50); // volumeG refere-se ao volume global
        audioListen.volume = volumeG;
        if (volumeSlider != null)
        {
            volumeSlider.value = volumeG;
        }
        if (audioDeath != null)
        {
            audioDeath.volume = volumeG;
        }
        if (audioShield != null)
        {
            audioShield.volume = volumeG;
        }
    }
}
