using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FX_LevelComplete : MonoBehaviour , ICommondFX
{
    private Transform child;
    private void Start()
    {
        child = this.transform.GetChild(0);
        child.gameObject.SetActive(false);
        FX_ScreenEndGame.Instance.Add(this);
    }

    public float timeWaitng { get => 0; }
    public float timeDoing { get => 3; }
    public int priority { get => 0; }
    public void DoFX()
    {
        child.gameObject.SetActive(true);
        child.transform.DOScale(1, 0.3F).From(0);
    }
}
