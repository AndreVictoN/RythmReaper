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
private EnemyScript[] enemies;

[SerializeField] private int sceneIndex;
[SerializeField] private GameObject[] objectsToHide;

public TextMeshProUGUI textPointsGame;

public TextMeshProUGUI textPoints;
public TextMeshProUGUI textLife;

 
    void Start()
    {
        enemies = FindObjectsOfType<EnemyScript>();
    }

    void Update()
    {
        

       if(player.life <= 0 ){

        textPoints.text = textPointsGame.text;
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
    }

    public bool isOver(){
       if(player.life <= 0 ){
         return true;
       }

       return false;

    }
    public void MainMenuButton(){
        SceneManager.LoadScene(0);
    }

    public void ResetButton(){
        SceneManager.LoadScene(sceneIndex);
    }
    
    
}
