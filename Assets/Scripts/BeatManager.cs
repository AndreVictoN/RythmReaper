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
#region time
public class BeatManager : Singleton<BeatManager>
{
    [SerializeField] private AudioSource _audioSource;
    public List<double> beatTime;
    public EnemySpawner spawnerUp;
    public EnemySpawner spawnerLeft;
    public EnemySpawner spawnerRight;
    public EnemySpawner spawnerDown;
    public PulseToTheBeat pulse;
    public float timer = 0f;
    public float time = 4f;
    public int qntSpawn = 0;

    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();

        if(_audioSource.CompareTag("Song1"))
        {
            Song1Load();
        }
    }

    void Update()
    {
        _audioSource.volume = PlayerPrefs.GetFloat("Volume", 1f);
        
        if(_audioSource.CompareTag("Song1"))
        {
            Song1();
        }
    }

    private void Song1Load()
    {
        beatTime = new List<double>() {6.14399, 8.14932, 12.16, 16.128, 17.1307, 18.1333, 18.88, 19.136, 19.6693, 20.16, 21.248, 22.1653, 22.4, 23.168, 24.128, 24.384, 24.9387, 25.152, 26.1333, 27.136, 27.6266, 28.16, 28.416, 28.8853, 29.1626, 30.208, 33.1946, 34.1547, 34.9653, 35.1573, 35.648, 36.1387, 37.1627, 38.1653, 38.4213, 39.168, 40.1707, 40.4266, 40.896, 41.1946, 43.136, 43.6693, 44.1813, 44.3946, 44.9066, 46.1653, 48.2346, 49.2373, 50.2187, 50.9013, 51.1573, 51.6693, 52.2666, 53.2053, 54.4213, 56.192, 56.4053, 56.896, 57.1946, 58.176, 59.1573, 59.648, 60.1813, 60.416, 60.9066, 61.184, 62.1866, 64.1706, 65.1733, 66.176, 66.9013, 67.1573, 67.6693, 68.16, 69.248, 70.208, 70.4426, 71.2747, 72.4266, 72.96, 74.176, 75.1573, 75.6693, 76.1813, 76.416, 76.9493, 77.184, 78.1867, 80.256, 81.1946, 82.1973, 82.944, 83.1787, 83.6693, 84.2027, 85.184, 86.1653, 86.4213, 87.1893, 88.1706, 88.5333, 88.9173, 89.216, 90.2613, 91.1787, 91.6907, 92.2666, 92.4587, 92.928, 93.2053, 94.208, 96.1706, 97.1733, 100.565, 101.184, 104.171, 104.683, 105.173, 114.219, 114.944, 115.179, 115.691, 116.203, 117.205, 118.443, 119.232, 120.469, 120.939, 122.219, 123.2, 123.712, 124.224, 124.459, 125.056, 125.248, 126.208, 128.235, 129.216, 130.944, 131.2, 131.712, 132.224, 133.333, 134.251, 134.464, 135.253, 136.213, 136.448, 136.981, 137.216, 138.197, 139.2, 139.691, 140.224, 140.48, 140.949, 141.227, 142.293, 145.216, 146.197, 146.987, 147.2, 147.712, 148.224, 149.269, 150.272, 150.485, 151.275, 152.235, 152.469, 152.96, 153.259, 155.2, 155.733, 156.245, 156.459, 156.992, 158.251, 162.261, 162.965, 163.221, 163.733, 164.331, 165.269, 166.379, 166.549, 167.275, 168.256, 168.469, 168.96, 169.301, 171.221, 171.776, 172.245, 172.48, 172.971, 173.227, 174.229, 176.235, 180.245, 182.251, 183.253, 184.235, 188.245, 192.256};
        for(int i = 0; i <= 210; i++)
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

    private void Song1()
    {
        foreach(double d in beatTime)
        {
            if(_audioSource.time > (d - 0.1) - 0.5 && _audioSource.time < (d + 0.1) - 0.5 /*&& f != 4 && f != 44 && f != 60 && f != 100 && f != 116*/)
            {
                if(timer <= 0f)
                {
                    //pulse.Pulse();
                    //Debug.Log(f);

                    if(qntSpawn % 2 == 0)
                    {
                        spawnerRight.newEnemy();
                    }else
                    {
                        spawnerUp.newEnemy();
                    }

                    qntSpawn++;
                    timer = 0.2f;
                }
            }
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
#endregion

#region BPM
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
#endregion

#region Spectrum
/*
public class BeatManager : Singleton<BeatManager>
{
    public float[] spectrum = new float[256];
    public PulseToTheBeat pulse;
    public EnemySpawner spawnerUp;
    public EnemySpawner spawnerLeft;
    public EnemySpawner spawnerRight;
    public EnemySpawner spawnerDown;
    [SerializeField] private AudioSource _audioSource;

    public float timer = 0f;

    void Update()
    {
        if(_audioSource.CompareTag("Song1"))
        {
            Song1();
        }
    }

    private void Song1()
    {
        //AudioListener.GetSpectrumData();
        _audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for(int i = 0; i < spectrum.Length; i++)
        {
            float tmp = spectrum[i] * 15;

            //Debug.Log(tmp);

            if(tmp >= 2f)
            {
                if(timer <= 0f)
                {
                    Debug.Log(_audioSource.time);
                    spawnerRight.newEnemy();
                    pulse.Pulse();

                    timer = 0.3f;
                }
            }
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
*/
#endregion