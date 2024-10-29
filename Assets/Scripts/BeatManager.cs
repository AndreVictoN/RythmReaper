using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Core.Singleton;
using JetBrains.Annotations;
using Reactional.Playback;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class BeatManager : Singleton<BeatManager>
{
    [SerializeField] private AudioSource _audioSource;
    //public float[] beatTime;
    public List<float> beatTime = new List<float>();
    public EnemySpawner spawnerFront;
    public EnemySpawner spawnerUp;
    public PulseToTheBeat pulse;
    public float timer = 0f;
    public float time = 4f;
    public int qntSpawn = 0;

    void Start()
    {
        for(int i = 0; i <= 232; i++)
        {
            if(time == 35 || time == 91)
            {
                beatTime.Add(time);

                time += 9;
            }else if(time == 58.5f || time == 114.5f)
            {
                beatTime.Add(time);

                time += 1.5f;
            }else
            {
                beatTime.Add(time);

                time += 0.5f;
            }
        }

        //Debug.Log(time - 0.5f);
    }

    void Update()
    {
        /*for(int i = 0; i < beatTime.Count; i++)
        {
            if(_audioSource.time )
            {

            }
        }*/

        foreach(float f in beatTime)
        {
            if(_audioSource.time > (f - 0.5) - 0.8 && _audioSource.time < (f + 0.5) - 0.8 && f != 4 && f != 44 && f != 60 && f != 100 && f != 116)
            {
                if(timer <= 0f)
                {
                    //pulse.Pulse();
                    //Debug.Log(f);

                    if(qntSpawn % 2 == 0)
                    {
                        spawnerFront.newEnemy();
                    }else
                    {
                        spawnerUp.newEnemy();
                    }

                    qntSpawn++;
                    timer = 0.5f;
                }
            }
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}

/*public class BeatManager : MonoBehaviour
{
    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;

    private void Update()
    {
        Debug.Log(_audioSource.time);

        if(_audioSource.time > 34)
        {
            foreach(Intervals interval in _intervals)
            {
                float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLenght(_bpm)));

                interval.CheckForNewInterval(sampledTime);
            }
        }
    }
}

[System.Serializable]
public class Intervals 
{
    [SerializeField] private float _steps;
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;

    public float GetIntervalLenght(float bpm)
    {
        return 60f / (bpm * _steps);
    }

    public void CheckForNewInterval (float interval)
    {
        if(Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(interval);

            _trigger.Invoke();
        }
    }
}*/

/*public class BeatManager : Singleton<BeatManager>
{
    public float[] spectrum = new float[256];
    public PulseToTheBeat pulse;
    //public EnemySpawner spawner;
    [SerializeField] private AudioSource _audioSource;

    public float timer = 0f;

    void Update()
    {
        //AudioListener.GetSpectrumData();
        _audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for(int i = 0; i < spectrum.Length; i++)
        {
            float tmp = spectrum[i] * 6;

            //Debug.Log(tmp);

            if(tmp >= 2f)
            {
                pulse.Pulse();
                if(timer <= 0f)
                {
                    Debug.Log(_audioSource.time);
                    //spawner.newEnemy();

                    timer = 0.2f;
                }
            }
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}*/