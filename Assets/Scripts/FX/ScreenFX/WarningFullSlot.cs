using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WarningFullSlot : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;

    public static WarningFullSlot Instance;
    private bool isDoing;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void DOFX()
    {
        if (isDoing)
            return;

        isDoing = true;
        _canvas.DOFade(0.3f, 0.5f).From(0).SetLoops(-1, LoopType.Yoyo);
    }

    public void StopFX()
    {
        _canvas.DOKill();
        isDoing = false;
        _canvas.alpha = 0;
    }
}
