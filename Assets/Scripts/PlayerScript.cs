using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

    // Armazena as teclas mapeadas
    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode rightKey;
    private KeyCode leftKey;

    void Start()
    {
        // Carrega as teclas salvas no PlayerPrefs ou usa valores padrão
        upKey = LoadKey("UpKey", KeyCode.W);
        downKey = LoadKey("DownKey", KeyCode.S);
        rightKey = LoadKey("RightKey", KeyCode.D);
        leftKey = LoadKey("LeftKey", KeyCode.A);

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
            tween.Kill();
            Destroy(gameObject);
        }
    }

    void SetMove()
    {
        if (Input.GetKeyDown(upKey) && timer <= 0)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            StartMove(new Vector3(0f, 3.53f, 0f));
            timer = 0.05f;
        }
        else if (Input.GetKeyDown(downKey) && timer <= 0)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            StartMove(new Vector3(0f, -3.53f, 0f));
            timer = 0.05f;
        }
        else if (Input.GetKeyDown(rightKey) && timer <= 0)
        {
            this.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            StartMove(new Vector3(3.53f, 0f, 0f));
            timer = 0.05f;
        }
        else if (Input.GetKeyDown(leftKey) && timer <= 0)
        {
            this.gameObject.transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            StartMove(new Vector3(-3.53f, 0f, 0f));
            timer = 0.05f;
        }

        if (!isMoving && !tween.IsActive() && this.gameObject.activeSelf)
        {
            transform.DOMove(new Vector3(0f, 0f, 0f), duration).SetEase(ease);
        }
    }

    void StartMove(Vector3 targetPosition)
    {
        if (this.gameObject.activeSelf)
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
