using System;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    public PlayerScript player;

    public GameObject playerObject;
    public BeatManager song;
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    private EnemyScript[] enemies;

    [SerializeField] private PauseMenu pause;


    [SerializeField] private int sceneIndex;
    [SerializeField] private GameObject[] objectsToHide;

    public TextMeshProUGUI textPointsGame;
    public TextMeshProUGUI[] textPoints;
    public TextMeshProUGUI textLife;

    public TextMeshProUGUI textLifeGame;
    public TextMeshProUGUI gradeText; 

    void Start()
    {
        enemies = FindObjectsOfType<EnemyScript>();
    }

    void Update()
    {
        enemies = FindObjectsOfType<EnemyScript>();

        if (isOver())
        {
            foreach (GameObject obj in objectsToHide)
            {
                obj.SetActive(false);
            }
        }


        if (player.life <= 0)
        {
            EndGame();
        }

        if ((!song._audioSource.isPlaying && player.life > 0 && !pause.isPaused) || Input.GetKeyDown(KeyCode.L))
        {
            WinGame();
        }
    }

    void EndGame()
    {
        foreach (TextMeshProUGUI finalPoints in textPoints)
        {
            finalPoints.text = textPointsGame.text;
        }

        textLife.text = textLifeGame.text;

        enemies = FindObjectsOfType<EnemyScript>();

        song._audioSource.Stop();

        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }

        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }

        gameOverScreen.SetActive(true);
    }

    void WinGame()
    {
        foreach (TextMeshProUGUI finalPoints in textPoints)
        {
            finalPoints.text = textPointsGame.text;
        }

        textLife.text = textLifeGame.text;

        enemies = FindObjectsOfType<EnemyScript>();

        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }

        song._audioSource.Stop();

        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }

       
        string grade = CalculateGrade(player.life);
        gradeText.text = "Grade: " + grade; 

        gameWinScreen.SetActive(true);
        playerObject.SetActive(false);
    }

    string CalculateGrade(int life)
    {
        if (life == 10)
        {
            return "S";
        }
        else if (life >= 7 && life <= 9)
        {
            return "A";
        }
        else if (life >= 4 && life <= 6)
        {
            return "B";
        }
        else if (life < 4)
        {
            return "C";
        }

        return "-"; 
    }

    public bool isOver()
    {
        if (player.life <= 0 || (!song._audioSource.isPlaying && player.life > 0 && !pause.isPaused))
        {
            return true;
        }

        return false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetButton()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
