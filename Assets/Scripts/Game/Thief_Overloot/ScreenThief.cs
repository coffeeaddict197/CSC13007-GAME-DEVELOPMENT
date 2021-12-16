using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenThief : MonoBehaviour
{
    [Header("Configs")] 
    public RectTransform bgScreen;
    public RectTransform thief;

    private void OnEnable()
    {
        bgScreen.DOLocalMoveX(0, 0.2f).From(3000f);
        thief.DOLocalMoveX(0, 0.2f).From(3000f);
    }
}
