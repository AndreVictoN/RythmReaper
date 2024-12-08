using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public bool isScaling = false;
    public bool scaled = false;
    public Vector3 originalScale;
    public Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;

        targetScale = new Vector3(originalScale.x + 0.10f, originalScale.y + 0.10f, originalScale.z + 0.10f);

        isScaling = false;
        scaled = false;
    }

    void Update()
    {
        if(isScaling && !scaled)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, 2f);

            scaled = true;
        }else if(!isScaling && scaled)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, 2f);
            
            scaled = false;
        }
    }

    public void ScaleUp()
    {
        isScaling = true;
    }
}
