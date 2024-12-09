using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    
    public void newEnemy()
    {
        Instantiate(enemy);
    }
}
