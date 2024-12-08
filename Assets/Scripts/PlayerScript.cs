using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float duration = 0.15f;
    public Tween tween;
    public Tween tweenMoveCenter;
    public Ease ease = Ease.InOutSine;
    public GameManager gameManager;

    public bool isMoving = false;
    public float timer = 0f;

    public int life = 10;

    public bool isDestroyed = false;

    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode rightKey;
    private KeyCode leftKey;

    public AudioSource src;
    public AudioClip sfxH;

    void Start()
    {
        upKey = LoadKey("UpKey", KeyCode.W);
        downKey = LoadKey("DownKey", KeyCode.S);
        rightKey = LoadKey("RightKey", KeyCode.D);
        leftKey = LoadKey("LeftKey", KeyCode.A);

        isDestroyed = false;

        if (life != 10)
        {
            life = 10;
        }
    }

    void Update()
    {
        src.volume = PlayerPrefs.GetFloat("Volume", 1f) + 0.5f;

        upKey = LoadKey("UpKey", KeyCode.W);
        downKey = LoadKey("DownKey", KeyCode.S);
        rightKey = LoadKey("RightKey", KeyCode.D);
        leftKey = LoadKey("LeftKey", KeyCode.A);
        SetMove();

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        if (life == 0)
        {
            DestroyPlayer(gameObject);
        }
    }

    public void DestroyPlayer(GameObject gobj)
    {
        Destroy(gobj);
    }

    public void OnDestroy()
    {
        tween?.Kill();
        tweenMoveCenter?.Kill();
        DOTween.Kill(gameObject);
        isDestroyed = true;
    }

    void SetMove()
    {
        if(isDestroyed) return;

        if (Input.GetKeyDown(upKey) && timer <= 0 && !isDestroyed)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("AttckUD");
            StartMove(new Vector3(0f, 3.53f, 0f));
            this.gameObject.transform.localScale = new Vector3(4.3f, -4.3f, 4.3f);
            timer = 0.05f;
            Invoke("CorrectScale", 0.3f);
        }
        else if (Input.GetKeyDown(downKey) && timer <= 0 && !isDestroyed)
        {
            CorrectScale();
            this.gameObject.transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
            this.gameObject.GetComponent<Animator>().SetTrigger("AttckUD");
            StartMove(new Vector3(0f, -3.53f, 0f));
            timer = 0.05f;
        }
        else if (Input.GetKeyDown(rightKey) && timer <= 0 && !isDestroyed)
        {
            CorrectScale();
            this.gameObject.transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            StartMove(new Vector3(3.53f, 0f, 0f));
            timer = 0.05f;
        }
        else if (Input.GetKeyDown(leftKey) && timer <= 0 && !isDestroyed)
        {
            CorrectScale();
            this.gameObject.transform.localScale = new Vector3(-4.3f, 4.3f, 4.3f);
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            StartMove(new Vector3(-3.53f, 0f, 0f));
            timer = 0.05f;
        }

        if (!isMoving && this.gameObject.activeSelf && !isDestroyed)
        {
            tweenMoveCenter?.Kill();
            tweenMoveCenter = transform.DOMove(new Vector3(0f, 0f, 0f), duration).SetEase(ease);
        }
    }

    void StartMove(Vector3 targetPosition)
    {
        if (!isDestroyed)
        {
            isMoving = true;

            tween?.Kill();
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

    public void ResetLife()
    {
        life = 10;
    }

    private KeyCode LoadKey(string keyName, KeyCode defaultKey)
    {
        if (PlayerPrefs.HasKey(keyName))
        {
            return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyName));
        }
        return defaultKey;
    }

    public void PlayHitSFX()
    {
        src.clip = sfxH;

        src.Play();
    }

    public void CorrectScale()
    {
        this.gameObject.transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
    }
}
