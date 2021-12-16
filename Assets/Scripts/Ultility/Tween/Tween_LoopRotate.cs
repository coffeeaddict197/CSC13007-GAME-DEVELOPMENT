using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tween_LoopRotate : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.DORotate(new Vector3(0,0,360f), 2F, RotateMode.FastBeyond360).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);
    }

}
