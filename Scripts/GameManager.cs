using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textPoints;
    public int score = 0;

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        textPoints.text = "0";
        score = 0;
    }

    public void UpdatePoints()
    {
        score++;

        textPoints.text = score.ToString();
    }
}
