using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public static Action<int,int> onUpdateProgress;

    private void OnEnable()
    {
        onUpdateProgress += UpdateProgress;
    }

    private void OnDisable()
    {
        onUpdateProgress -= UpdateProgress;
    }

    void UpdateProgress(int cur, int max)
    {
        slider.DOValue((float) cur / max, 0.5f);
    }

}
