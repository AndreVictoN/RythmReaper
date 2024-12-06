using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DOTweenManager : Singleton<DOTweenManager>
{
    void Awake()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly).SetCapacity(500,50);
    }
}
