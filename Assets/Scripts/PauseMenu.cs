using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Scripting.APIUpdating;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private GameObject audioPanel; 

    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private GameObject[] objectsToHide;

    private EnemyScript[] enemies;
    private bool isPaused = false;

    void Start()
    {
        enemies = FindObjectsOfType<EnemyScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(true);
        }  

        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        } 
        
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;         
        backgroundMusic.UnPause();   
        isPaused = false;
        audioPanel.SetActive(false);
    }

    public void Pause()
    {
        enemies = FindObjectsOfType<EnemyScript>();
        
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }

        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f;        
        backgroundMusic.Pause();    
        isPaused = true;
    }

    public void QuitGame()
    {
        
        Debug.Log("Quitting the game...");
        Application.Quit(); 
    }

    public void OpenAudioScreen(){
        audioPanel.SetActive(true);
    }

        public void CloseAudioScreen(){
        audioPanel.SetActive(false);
    }

        public bool IsPaused()
    {
        return isPaused; // Retorna o estado atual de pausa
    }
}
