using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;

public class EnemyScript : MonoBehaviour
{
    public float duration = 0.15f;
    public float timeToDestroy = 0.30f;
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

                MoveToPlayer();
            }
        }else if(this.gameObject.tag == "EnemyLeft")
        {
            if(movingToTarget && Vector3.Distance(transform.position, new Vector3(-1.98f,0.7525349f,0f)) < 0.01f)
            {
                movingToTarget = false;

                MoveToPlayer();
            }
        }else if(this.gameObject.tag == "EnemyTop")
        {
            if(movingToTarget && Vector3.Distance(transform.position, new Vector3(0f,3.53f,0f)) < 0.01f)
            {
                movingToTarget = false;

                MoveToPlayer();
            }
        }else if(this.gameObject.tag == "EnemyDown")
        {
            if(movingToTarget && Vector3.Distance(transform.position, new Vector3(0f,-3.53f,0f)) < 0.01f)
            {
                movingToTarget = false;

                MoveToPlayer();
            }
        }

        if(this.gameObject.transform.position == new Vector3(0f,0f,0f) || this.gameObject.transform.position == new Vector3(1.56f, 0.7525349f, 0f))
        {
            //Destroy(gameObject);

            DestroyEnemy();
        }
    }

    public void MoveToPlayer()
    {
        if(this.gameObject.transform != null && _tween.IsActive())
        {
            _tween?.Kill(); 
        }

        if(_tween == null || !_tween.IsActive())
        {
            _tween?.Kill();

            if(player.activeSelf && this.gameObject.tag != "EnemyLeft")
            {
                _tween = transform.DOMove(new Vector3(0f,0f,0f), duration / 2);
            }else{
                _tween = transform.DOMove(new Vector3(1.56f,0.7525349f,0f), duration / 2);
            }
        }

        /*if(this.gameObject.CompareTag("EnemyRight"))
        {
            _tween = transform.DOMove(new Vector3(0f,0f,0f), duration / 2);
        }else if(this.gameObject.CompareTag("EnemyTop"))
        {
            _tween = transform.DOMove(new Vector3(0f,0f,0f), duration / 2);
        }*/
    }

    public void Move(string tag)
    {
        if(gameObject.transform == null) return;

        //if(_tween?.IsActive() == true) _tween.Kill();

        if(_tween == null || !_tween.IsPlaying())
        {
            _tween?.Kill();

            switch (tag)
            {
                case "EnemyRight":
                    _tween = transform.DOMove(new Vector3(3.53f, 0f, 0f), duration).OnKill(() => movingToTarget = false);
                    break;
                case "EnemyLeft":
                    _tween = transform.DOMove(new Vector3(-1.98f, 0.7525349f, 0f), duration).OnKill(() => movingToTarget = false);
                    break;
                case "EnemyTop":
                    _tween = transform.DOMove(new Vector3(0f, 3.53f, 0f), duration).OnKill(() => movingToTarget = false);
                    break;
                case "EnemyDown":
                    _tween = transform.DOMove(new Vector3(0f, -3.53f, 0f), duration).OnKill(() => movingToTarget = false);
                    break;
            }
        }

        /*if(tag == "EnemyRight")
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

            _tween = transform.DOMove(new Vector3(-1.98f, 0.7525349f, 0f), duration).OnKill(() => {movingToTarget = false;});
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
        }*/
    }

    void OnDestroy()
    {
        _tween?.Kill();

        DOTween.Kill(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.activeSelf)
        {
            if(this.gameObject.tag == "EnemyRight")
            {
                if(this.transform.position.x >= 3.41f)
                {
                    Destroy(this.gameObject);

                    collision.gameObject.GetComponent<PlayerScript>().AddPoints();
                }else if(this.gameObject.transform.position.x < 3.41f)
                {
                    collision.gameObject.GetComponent<PlayerScript>().Damage();
                    collision.gameObject.GetComponent<PlayerScript>().PlayHitSFX();
                    Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
                }
            }else if(this.gameObject.tag == "EnemyLeft")
            {
                if(this.transform.position.x <= -1.32f)
                {
                    Destroy(this.gameObject);

                    collision.gameObject.GetComponent<PlayerScript>().AddPoints();
                }else if(this.gameObject.transform.position.x > -1.32f)
                {
                    collision.gameObject.GetComponent<PlayerScript>().Damage();
                    collision.gameObject.GetComponent<PlayerScript>().PlayHitSFX();
                    Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
                }
            }else if(this.gameObject.tag == "EnemyTop")
            {
                if(this.transform.position.y >= 3.41f)
                {
                    Destroy(this.gameObject);

                    collision.gameObject.GetComponent<PlayerScript>().AddPoints();
                }else if(this.gameObject.transform.position.y < 3.41f)
                {
                    collision.gameObject.GetComponent<PlayerScript>().Damage();
                    collision.gameObject.GetComponent<PlayerScript>().PlayHitSFX();
                    Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
                }
            }else if(this.gameObject.tag == "EnemyDown")
            {
                if(this.transform.position.y <= -3.41f)
                {
                    Destroy(this.gameObject);

                    collision.gameObject.GetComponent<PlayerScript>().AddPoints();
                }else if(this.gameObject.transform.position.y > -3.41f)
                {
                    collision.gameObject.GetComponent<PlayerScript>().Damage();
                    collision.gameObject.GetComponent<PlayerScript>().PlayHitSFX();
                    Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
                }
            }
        }

        if(collision.gameObject.tag == "EnemyLeft" || collision.gameObject.tag == "EnemyRight" || collision.gameObject.tag == "EnemyDown" || collision.gameObject.tag == "EnemyTop")
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    public void DestroyEnemy()
    {
        //this.gameObject.GetComponent<Animator>().SetTrigger("Dying");

        Destroy(this.gameObject/*, timeToDestroy*/);
    }
}
