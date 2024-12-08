using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public GameObject images;
    public GameObject corners;
    public TextMeshProUGUI enter;
    public int timesEnter = 0;
    public int currentImage = 1;

    public AudioSource src;
    public AudioClip beatbox;
    public AudioClip portal;

    void Start()
    {
        timesEnter = 0;
        currentImage = 1;

        Invoke("PlayBeatbox", 2f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            JumpImage();

            timesEnter++;
        }

        if(timesEnter == 1 || timesEnter == 2 || timesEnter == 3)
        {
            if(src.clip == beatbox)
            {
                src.Stop();
            }
        }
    }

    public void JumpImage()
    {
        switch (timesEnter)
        {
            case 0:
                currentImage = timesEnter + 2;
                src.Stop();
                images.GetComponent<Animator>().SetTrigger("Load2");
                Debug.Log("Image: " + currentImage);
                break;
            case 1:
                currentImage = timesEnter + 2;
                Invoke("PlayPortal", 1f);
                images.GetComponent<Animator>().SetTrigger("Load3");
                Debug.Log("Image: " + currentImage);
                break;
            case 2:
                currentImage = timesEnter + 2;
                images.GetComponent<Animator>().SetTrigger("Load4");
                Debug.Log("Image: " + currentImage);
                break;
            case 3:
                currentImage = timesEnter + 2;
                images.GetComponent<Animator>().SetTrigger("Load5");
                corners.GetComponent<Animator>().SetTrigger("Exit");
                enter.GetComponent<Animator>().SetTrigger("Out");
                Invoke("GoToMenu", 1f);
                Debug.Log("Image: " + currentImage);
                break;

        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayBeatbox()
    {
        src.volume = 1f;

        src.clip = beatbox;

        src.Play();

        src.loop = false;
    }

    public void PlayPortal()
    {
        src.volume = 0.5f;
        
        src.clip = portal;

        src.Play();

        src.loop = true;
    }
}
