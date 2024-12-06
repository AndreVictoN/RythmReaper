using System;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{

public PlayerScript player;
public BeatManager song;
public GameObject gameOverScreen;
public GameObject gameWinScreen;
[SerializeField] private EnemyScript[] enemies;

[SerializeField] private int sceneIndex;
[SerializeField] private GameObject[] objectsToHide;

public TextMeshProUGUI textPointsGame;
public TextMeshProUGUI textPoints;
public TextMeshProUGUI textLife;

public bool quitting = false;

public PauseMenu pauseMenu;
 
    void Start()
    {
        enemies = FindObjectsOfType<EnemyScript>();
        quitting = false;
    }

    void Update()
    {
        enemies = FindObjectsOfType<EnemyScript>();

        if(player.life <= 0){

            textPoints.text = textPointsGame.text;

            song._audioSource.Stop();
        
            foreach (GameObject obj in objectsToHide)
            {
                obj.SetActive(false);
            }

            foreach (var enemy in enemies)
            {
            enemy.gameObject.SetActive(false);
            }

            if(!quitting)
            {
                gameOverScreen.SetActive(true);
            }else
            {
                MainMenuButton();
            }
        }

        /*if(quitting == true)
        {
            MainMenuButton();
        }*/
    }

    public bool isOver(){
       if(player.life <= 0 ){
         return true;
       }

       return false;

    }
    public void MainMenuButton(){
        if(player != null)
        {
            player.gameObject.SetActive(true);
            player.life = 0;

            Time.timeScale = 1f;

            pauseMenu.isPaused = false;

            quitting = true;
        }else if(player == null)
        {
            quitting = false;
            SceneManager.LoadScene(0);
        }
    }

    public void ResetButton(){
        SceneManager.LoadScene(sceneIndex);
    }
}
