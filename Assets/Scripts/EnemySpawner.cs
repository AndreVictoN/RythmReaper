using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float time = 1;
    public GameObject enemy;

    public float countRight;
    public float countLeft;
    public float countUp;
    public float countDown;

    void Update()
    {
        newEnemy();
    }
    
    public void newEnemy()
    {
        /*if(!GameObject.Find("Enemy"))
        {
            count += Time.deltaTime;

            if(count > time)
            {
                Instantiate(enemy);

                count = 0;
            }
        }*/

        if(enemy.CompareTag("EnemyRight"))
        {
            countRight += Time.deltaTime;

            if(countRight > time)
            {
                Instantiate(enemy);

                countRight = 0;
            }
        }else if(enemy.CompareTag("EnemyLeft"))
        {
            countLeft += Time.deltaTime;

            if(countLeft > time)
            {
                Instantiate(enemy);

                countLeft = 0;
            }
        }else if(enemy.CompareTag("EnemyTop"))
        {
            countUp += Time.deltaTime;

            if(countUp > time)
            {
                Instantiate(enemy);

                countUp = 0;
            }
        }else if(enemy.CompareTag("EnemyDown"))
        {
            countDown += Time.deltaTime;

            if(countDown > time)
            {
                Instantiate(enemy);

                countDown = 0;
            }
        }
    }
}
