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
using Random = UnityEngine.Random;

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
    public int random;

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
        beatTime = new List<double>() {3.34932, 4.52265, 6.89066, 7.76533, 10.0266, 11.2, 13.248, 14.08, 16.1493, 17.344, 19.3707, 20.2027, 22.4426, 23.0187, 23.5947, 24.448, 25.3653, 25.7707, 26.1546, 26.88, 27.6907, 28.2027, 28.608, 29.1626, 29.568, 30.1013, 30.912, 31.8293, 32.256, 32.768, 33.344, 34.176, 35.264, 35.6693, 35.2213, 35.52, 36.9707, 38.9973, 39.5947, 40.2133, 42.2613, 42.88, 43.1573, 43.456, 45.4827, 46.1013, 46.5707, 46.848, 48.7253, 48.8533, 49.152, 49.4507, 49.536, 49.9413, 50.1973, 50.5813, 50.7307, 50.88, 51.2, 51.4773, 51.7973, 52.096, 52.3946, 52.6933, 52.9493, 53.184, 53.44, 53.824, 54.2293, 54.4213, 54.72, 55.0187, 55.4026, 55.6373, 55.9146, 56.192, 56.4266, 56.6827, 57.0666,57.216, 57.344, 57.6427, 57.9627, 58.2613, 58.56, 58.5813, 58.88, 59.1787, 59.4347, 59.6693, 59.9253, 60.3093, 60.4587, 60.5866, 60.9066, 61.2053, 61.504, 61.8027, 62.08, 62.1227, 62.4, 62.656, 62.912, 63.168, 63.552, 63.68, 63.8507, 64.128, 64.4693, 64.768, 65.088, 65.3226, 65.5573, 65.856, 66.1546, 66.4107, 66.7946, 66.9227, 67.0933, 67.392, 67.6907, 67.9893, 68.288, 68.5653, 68.8, 69.0347, 69.3973, 69.568, 70.08, 71.808, 72.2346, 74.624, 75.0293, 75.4773, 76.3307, 77.2266, 78.1227, 78.784, 79.5947, 80.5333, 81.0453, 81.536, 81.984, 82.816, 83.9466, 84.672, 85.2693, 86.0587, 86.8693, 87.9146, 88.4906, 89.3226, 92.5013, 92.9707, 94.1013, 95.7013, 98.9227, 103.872, 104, 104.491, 104.704, 105.088, 105.365, 105.707, 105.984, 106.091, 106.304, 106.603, 106.901, 107.243, 107.712, 108.053, 108.331, 108.544, 108.949, 109.077, 109.248, 109.547, 109.867, 110.144, 110.485, 110.784, 111.083, 111.125, 111.573, 111.787, 112.192, 112.32, 112.491, 112.789, 113.088, 113.387, 113.728, 114.197, 114.368, 114.816, 115.029, 115.435, 115.563, 115.733, 116.032, 116.331, 116.672, 116.864, 116.949, 117.44, 117.568, 118.059, 118.187, 118.677, 118.976, 119.275, 119.595, 119.872, 120.171, 120.533, 120.683, 121.067, 121.301, 121.579, 121.92, 122.091, 122.219, 122.539, 122.837, 123.136, 123.456, 123.776, 123.925, 124.267, 124.544, 124.843, 125.163, 125.44, 125.525, 125.76, 126.059, 126.357, 126.677, 127.061, 127.168, 127.573, 127.787, 128.064, 128.405, 128.768, 129.92, 130.133, 130.667, 131.456, 132.395, 133.376, 133.909, 134.72, 135.787, 136.213, 136.597, 137.131, 137.941, 138.88, 139.648, 140.416, 141.056, 141.717, 142.933, 143.659, 144.363, 144.96, 146.112, 146.901, 147.541, 148.203, 149.355, 149.867, 150.4, 150.912, 151.445, 152.661, 153.387, 154.091, 154.624, 156.288, 156.779, 157.291, 157.888, 159.339, 159.829, 160.384, 160.896, 162.581, 163.072, 163.605, 164.117, 166.016, 166.507, 167.019, 167.531, 168.853, 168.96, 169.536, 170.368, 172.096, 172.949, 173.44, 173.973, 175.339, 176.192, 176.683, 177.216, 178.581, 179.435, 179.925, 180.459, 181.824, 182.677, 183.168, 183.701, 185.067, 185.195, 185.771, 186.603, 187.52, 187.925, 188.288, 189.099, 189.803};

        //Debug.Log(time - 0.5f);
    }

    private void Song1()
    {
        foreach(double d in beatTime)
        {
            if(_audioSource.time > (d - 0.1) - 0.8 && _audioSource.time < (d + 0.1) - 0.8)
            {
                if(timer <= 0f)
                {
                    pulse.Pulse();
                    Debug.Log(d);

                    random = Random.Range(0, 10);

                    Debug.Log(random);

                    if(random <= 5)
                    {
                        if(qntSpawn % 2 == 0)
                        {
                            spawnerRight.newEnemy();
                        }else
                        {
                            spawnerUp.newEnemy();
                        }
                    }else if(random > 5)
                    {
                        if(qntSpawn % 2 == 0)
                        {
                            spawnerDown.newEnemy();
                        }else
                        {
                            spawnerLeft.newEnemy();
                        }
                    }

                    qntSpawn++;
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
            float tmp = spectrum[i] * 20;

            //Debug.Log(tmp);

            if(tmp >= 1f)
            {
                if(timer <= 0f)
                {
                    Debug.Log(_audioSource.time);
                    //spawnerRight.newEnemy();
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