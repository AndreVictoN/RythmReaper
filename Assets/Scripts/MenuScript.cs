using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
     [SerializeField] private GameObject painelMenuInicial;
     [SerializeField] private GameObject painelOpcoes;
     [SerializeField] private GameObject audioScreen;

     [SerializeField] private GameObject audioButton;

     [SerializeField] private GameObject exitButton;

     [SerializeField] private GameObject OptionsTitle;

     public void LoadScene(int indexScene){
          SceneManager.LoadScene(indexScene);
     }

     public void ExitGame(){
          Application.Quit();
          Debug.Log("SAIU");         
     }

     public void OpenOption(){
          painelMenuInicial.SetActive(false);
          painelOpcoes.SetActive(true);
     }

     public void ExitOptions(){
          painelMenuInicial.SetActive(true);
          painelOpcoes.SetActive(false);
     }

     public void OpenAudioScreen(){
          audioScreen.SetActive(true);
          audioButton.SetActive(false);
          OptionsTitle.SetActive(false);
          exitButton.SetActive(false);  
     }
     

     public void ExitAudioScreen(){
          audioScreen.SetActive(false);
          audioButton.SetActive(true);
          OptionsTitle.SetActive(true);
          exitButton.SetActive(true);  
     }
}
