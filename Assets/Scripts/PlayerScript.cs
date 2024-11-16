using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float duration = 0.15f;

    public Tween tween;

    public Ease ease = Ease.InOutSine;

    public GameManager gameManager;

    public bool isMoving = false;

    public float timer = 0f;

    public int life = 10;

    void Start()
    {
        if(life != 10)
        {
            life = 10;
        }
    }

    void Update()
    {
        SetMove();

        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        if(life == 0)
        {
            tween.Kill();

            Destroy(gameObject);

            SceneManager.LoadScene(0);
        }
    }

    void SetMove()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && timer <= 0)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");

            StartMove(new Vector3(0f, 3.53f, 0f));

            timer = 0.05f;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && timer <= 0)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");

            StartMove(new Vector3(0f, -3.53f, 0f));

            timer = 0.05f;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && timer <= 0)
        {
            this.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            //StartMove(new Vector3(7.19f, 0f, 0f));
            StartMove(new Vector3(3.53f, 0f, 0f));

            timer = 0.05f;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && timer <= 0)
        {

            this.gameObject.transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            //StartMove(new Vector3(-7.19f, 0f, 0f));
            StartMove(new Vector3(-3.53f, 0f, 0f));
            
            timer = 0.05f;
        }
        /*else
        {
            transform.DOMove(new Vector3(0f, 0f, 0f), duration).SetEase(ease);
        }*/
        if(!isMoving && !tween.IsActive() && this.gameObject.activeSelf)
        {
            transform.DOMove(new Vector3(0f, 0f, 0f), duration).SetEase(ease);
        }
    }

    void StartMove(Vector3 targetPosition)
    {
        if(this.gameObject.activeSelf)
        {
            isMoving = true;

            tween = transform.DOMove(targetPosition, duration).SetEase(ease).OnComplete(() => isMoving = false);
        }
    }

    public void AddPoints()
    {
        gameManager.UpdatePoints();
    }

    public void Damage()
    {
        life -= 1;

        this.gameObject.GetComponent<Animator>().SetTrigger("Damage");
    }
}
