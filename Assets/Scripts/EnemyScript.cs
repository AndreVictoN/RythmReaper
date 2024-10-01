using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;

public class EnemyScript : MonoBehaviour
{
    public float duration = 0.15f;
    public bool movingToTarget = true;
    public GameObject player;

    private Tween _tween;

    void Start()
    {
        Move(this.gameObject.tag);
    }

    void Update()
    {
        if(this.gameObject.tag == "EnemyRight")
        {
            if(movingToTarget && Vector3.Distance(transform.position, new Vector3(3.53f,0f,0f)) < 0.01f)
            {
                movingToTarget = false;

                MoveToMiddle();
            }
        }else if(this.gameObject.tag == "EnemyLeft")
        {
            if(movingToTarget && Vector3.Distance(transform.position, new Vector3(-3.53f,0f,0f)) < 0.01f)
            {
                movingToTarget = false;

                MoveToMiddle();
            }
        }else if(this.gameObject.tag == "EnemyTop")
        {
            if(movingToTarget && Vector3.Distance(transform.position, new Vector3(0f,3.53f,0f)) < 0.01f)
            {
                movingToTarget = false;

                MoveToMiddle();
            }
        }else if(this.gameObject.tag == "EnemyDown")
        {
            if(movingToTarget && Vector3.Distance(transform.position, new Vector3(0f,-3.53f,0f)) < 0.01f)
            {
                movingToTarget = false;

                MoveToMiddle();
            }
        }

        if(this.gameObject.transform.position == new Vector3(0f,0f,0f))
        {
            Destroy(gameObject);
        }
    }

    public void MoveToMiddle()
    {
        if(_tween != null)
        {
            _tween.Kill();
        }

        _tween = transform.DOMove(new Vector3(0f,0f,0f), duration / 2);
    }

    public void Move(string tag)
    {
        if(tag == "EnemyRight")
        {
            if(_tween != null)
            {
                _tween.Kill();
            }

            _tween = transform.DOMove(new Vector3(3.53f, 0f, 0f), duration).OnKill(() => {movingToTarget = false;});
            //transform.position = Vector3.Lerp(transform.position, new Vector3(3.53f, 0f, 0f), Time.deltaTime);
        }else if(tag == "EnemyLeft")
        {
            if(_tween != null)
            {
                _tween.Kill();
            }

            _tween = transform.DOMove(new Vector3(-3.53f, 0f, 0f), duration).OnKill(() => {movingToTarget = false;});
        }else if(tag == "EnemyTop")
        {
            if(_tween != null)
            {
                _tween.Kill();
            }

            _tween = transform.DOMove(new Vector3(0f, 3.53f, 0f), duration).OnKill(() => {movingToTarget = false;});
        }else if(tag == "EnemyDown")
        {
            if(_tween != null)
            {
                _tween.Kill();
            }

            _tween = transform.DOMove(new Vector3(0f, -3.53f, 0f), duration).OnKill(() => {movingToTarget = false;});
        }
    }

    void OnDestroy()
    {
        if(_tween != null)
        {
        _tween.Kill();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(this.gameObject.tag == "EnemyRight")
            {
                if(this.transform.position.x >= 3.41f)
                {
                    Destroy(this.gameObject);
                }
            }else if(this.gameObject.tag == "EnemyLeft")
            {
                if(this.transform.position.x <= -3.41f)
                {
                    Destroy(this.gameObject);
                }
            }else if(this.gameObject.tag == "EnemyTop")
            {
                if(this.transform.position.y >= 3.34f)
                {
                    Destroy(this.gameObject);
                }
            }else if(this.gameObject.tag == "EnemyDown")
            {
                if(this.transform.position.y <= 3.34f)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
