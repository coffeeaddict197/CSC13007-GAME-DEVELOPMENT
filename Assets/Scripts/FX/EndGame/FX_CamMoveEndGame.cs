using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FX_CamMoveEndGame : MonoBehaviour , ICommondFX
{
    private Camera cam;
    [SerializeField] private Animator camController;
    [SerializeField] private RectTransform gameGrid;
    //Position -1.35 , 4.8 
    //size -> 3
    
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        FX_ScreenEndGame.Instance.Add(this);
    }

    private void OnDisable()
    {
        FX_ScreenEndGame.Instance.Remove(this);
    }
    
    public float timeWaitng { get; }
    public float timeDoing
    {
        get => 1f;
    }
    public int priority
    {
        get => 2;
    }
    public void DoFX()
    {
        camController.SetTrigger("Move");
        gameGrid.DOAnchorPosY(-4500f, 0.3f).SetEase(Ease.Linear);
        Player.Instance.DisableAllStuffAndUI();
        FX_ScreenEndGame.Instance.DoFX();
    }
}
