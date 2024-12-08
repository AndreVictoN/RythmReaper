using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSongManager : MonoBehaviour
{
    public AudioSource backgroundSong;
    void Start()
    {
        backgroundSong.volume = PlayerPrefs.GetFloat("Volume", 1f) * 0.2f;

        Invoke("PlaySong", 1f);
    }

    void Update()
    {
        backgroundSong.volume = PlayerPrefs.GetFloat("Volume", 1f) * 0.2f;
    }

    public void PlaySong()
    {
        backgroundSong.Play();
    }
}
