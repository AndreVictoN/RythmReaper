using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
     public void LoadScene(int indexScene){
          SceneManager.LoadScene(indexScene);
     }

     public void ExitGame(){
          Application.Quit();
          Debug.Log("SAIU");
     }
}
