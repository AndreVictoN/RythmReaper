using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class InputName : MonoBehaviour
{
    public string namePlayer;

    public void ReadStringInput(string s)
    {
        namePlayer = s;
    }
}
