using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu]
public class SOInt : ScriptableObject
{
    public int value;

    void Start()
    {
        value = 0;
    }
}
