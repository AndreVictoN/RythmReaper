using System;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TextMeshProUGUI textPoints;
    public SOInt soScore;

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        soScore.value = 0;
        
        textPoints.text = "0";
    }

    public void UpdatePoints()
    {
        soScore.value++;

        textPoints.text = soScore.value.ToString();
    }
}
