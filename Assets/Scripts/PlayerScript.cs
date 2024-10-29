using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Scripting.APIUpdating;

public class PlayerScript : MonoBehaviour
{
    public float duration = 0.15f;

    public Tween tween;

    public Ease ease = Ease.InOutSine;

    public GameManager gameManager;

    public bool isMoving = false;

    public float timer = 0f;

    void Update()
    {
        SetMove();

        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    void SetMove()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StartMove(new Vector3(-7f, 2f, 0f));
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartMove(new Vector3(0f, -3.53f, 0f));
        }
        else if(Input.GetKeyDown(KeyCode.J) && timer <= 0)
        {
            //StartMove(new Vector3(7.19f, 0f, 0f));
            StartMove(new Vector3(-7f, 0f, 0f));

            timer = 0.05f;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //StartMove(new Vector3(-7.19f, 0f, 0f));
            StartMove(new Vector3(-3.53f, 0f, 0f));
        }
        /*else
        {
            transform.DOMove(new Vector3(0f, 0f, 0f), duration).SetEase(ease);
        }*/
        if(!isMoving && !tween.IsActive())
        {
            transform.DOMove(new Vector3(-9f, 0f, 0f), duration).SetEase(ease);
        }
    }

    void StartMove(Vector3 targetPosition)
    {
        isMoving = true;

        tween = transform.DOMove(targetPosition, duration).SetEase(ease).OnComplete(() => isMoving = false);
    }

    public void AddPoints()
    {
        gameManager.UpdatePoints();
    }
}
