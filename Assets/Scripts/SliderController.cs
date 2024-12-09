using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Core.Singleton;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class SliderController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Carrega o valor do volume salvo, ou define para 1 (volume máximo)
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save(); // Salva as mudanças
    }
}
