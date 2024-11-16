using System;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TextMeshProUGUI textPoints;
    public TextMeshProUGUI textLife;
    public SOInt soScore;
    public PlayerScript player;
    
    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        textLife.text = player.life.ToString();
    }

    public void ResetGame()
    {
        soScore.value = 0;
        
        textPoints.text = "0";

        textLife.text = "10";
    }

    public void UpdatePoints()
    {
        soScore.value++;

        textPoints.text = soScore.value.ToString();
    }
}
