using UnityEngine;
using TMPro;

public class KeyBindingManager : MonoBehaviour
{
 
    public TextMeshProUGUI upButtonText;
    public TextMeshProUGUI downButtonText;
    public TextMeshProUGUI rightButtonText;
    public TextMeshProUGUI leftButtonText;


    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode rightKey;
    private KeyCode leftKey;

    private string currentAction = "";

    void Start()
    {

        upKey = LoadKey("UpKey", KeyCode.UpArrow);
        downKey = LoadKey("DownKey", KeyCode.DownArrow);
        rightKey = LoadKey("RightKey", KeyCode.RightArrow);
        leftKey = LoadKey("LeftKey", KeyCode.LeftArrow);

  
        UpdateButtonTexts();

        upButtonText.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => StartRemap("Up"));
        downButtonText.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => StartRemap("Down"));
        rightButtonText.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => StartRemap("Right"));
        leftButtonText.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => StartRemap("Left"));
    }

    void Update()
    {

        if (!string.IsNullOrEmpty(currentAction))
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    ApplyKeyRemap(key);
                    break;
                }
            }
        }
    }

    
    public void StartRemap(string action)
    {
        currentAction = action;
        Debug.Log($"Pressione uma tecla para remapear: {currentAction}");
    }

    
    private void ApplyKeyRemap(KeyCode newKey)
    {
        switch (currentAction)
        {
            case "Up":
                upKey = newKey;
                SaveKey("UpKey", newKey);
                break;
            case "Down":
                downKey = newKey;
                SaveKey("DownKey", newKey);
                break;
            case "Right":
                rightKey = newKey;
                SaveKey("RightKey", newKey);
                break;
            case "Left":
                leftKey = newKey;
                SaveKey("LeftKey", newKey);
                break;
        }

        currentAction = ""; 
        Debug.Log($"Tecla remapeada com sucesso! Nova tecla: {newKey}");
        UpdateButtonTexts();
    }

    
    private void UpdateButtonTexts()
    {
        upButtonText.text = $"UP KEY: {upKey}";
        downButtonText.text = $"DOWN KEY: {downKey}";
        rightButtonText.text = $"RIGHT KEY: {rightKey}";
        leftButtonText.text = $"LEFT KEY: {leftKey}";
    }


    private void SaveKey(string keyName, KeyCode key)
    {
        PlayerPrefs.SetString(keyName, key.ToString());
        PlayerPrefs.Save();
    }

    
    private KeyCode LoadKey(string keyName, KeyCode defaultKey)
    {
        if (PlayerPrefs.HasKey(keyName))
        {
            return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyName));
        }
        return defaultKey;
    }
}

