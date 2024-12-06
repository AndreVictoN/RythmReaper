using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    // Armazena as teclas mapeadas
    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode rightKey;
    private KeyCode leftKey;

    public bool isDestroyed = false;

    void Start()
    {
        // Carrega as teclas salvas no PlayerPrefs ou usa valores padrão
        upKey = LoadKey("UpKey", KeyCode.W);
        downKey = LoadKey("DownKey", KeyCode.S);
        rightKey = LoadKey("RightKey", KeyCode.D);
        leftKey = LoadKey("LeftKey", KeyCode.A);

        isDestroyed = false;

        SetMove();

        if (life != 10)
        {
            life = 10;
        }
    }

    void Update()
    {
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
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            StartMove(new Vector3(0f, 3.53f, 0f));
            timer = 0.05f;
        }
        else if (Input.GetKeyDown(downKey) && timer <= 0 && !isDestroyed)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            StartMove(new Vector3(0f, -3.53f, 0f));
            timer = 0.05f;
        }
        else if (Input.GetKeyDown(rightKey) && timer <= 0 && !isDestroyed)
        {
            this.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            StartMove(new Vector3(3.53f, 0f, 0f));
            timer = 0.05f;
        }
        else if (Input.GetKeyDown(leftKey) && timer <= 0 && !isDestroyed)
        {
            this.gameObject.transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
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

    /// <summary>
    /// Carrega uma tecla do PlayerPrefs ou retorna o valor padrão se não existir.
    /// </summary>
    /// <param name="keyName">Nome da chave a ser carregada.</param>
    /// <param name="defaultKey">Valor padrão caso não exista a chave.</param>
    /// <returns>A tecla carregada.</returns>
    private KeyCode LoadKey(string keyName, KeyCode defaultKey)
    {
        if (PlayerPrefs.HasKey(keyName))
        {
            return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyName));
        }
        return defaultKey;
    }
}
