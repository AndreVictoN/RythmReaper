using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
     [SerializeField] private GameObject painelMenuInicial;
     [SerializeField] private GameObject optionsScreen;
     [SerializeField] private GameObject audioScreen;



     [SerializeField] private GameObject selectLevel;

     [SerializeField] private GameObject audioButton;

     [SerializeField] private GameObject exitButton;



     [SerializeField] private GameObject ControlScreen;


     public void LoadScene(int indexScene){
          SceneManager.LoadScene(indexScene);
     }    

     public void pressPlayButton(){
          painelMenuInicial.SetActive(false);
          selectLevel.SetActive(true);
     }
     public void ExitGame(){
          Application.Quit();
          Debug.Log("SAIU");         
     }

     public void OpenOption(){
          painelMenuInicial.SetActive(false);
          optionsScreen.SetActive(true);
     }

     public void ExitOptions(){
          painelMenuInicial.SetActive(true);
          optionsScreen.SetActive(false);
     }

     public void OpenAudioScreen(){
          audioScreen.SetActive(true);
          
          optionsScreen.SetActive(false);
           
     }
     

     public void ExitAudioScreen(){
          audioScreen.SetActive(false);
          optionsScreen.SetActive(true);
     }

     public void ExitSelectScreen(){
          painelMenuInicial.SetActive(true);
          selectLevel.SetActive(false);
     }

     public void OpenControlScreen(){
          ControlScreen.SetActive(true);
          optionsScreen.SetActive(false);
     }

     public void ExitControlScreen(){
          ControlScreen.SetActive(!true);
          optionsScreen.SetActive(!false);
     }
}
