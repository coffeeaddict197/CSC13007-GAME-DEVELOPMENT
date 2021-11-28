using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FXFlashBag : MonoBehaviour
{
   [SerializeField] 
   private CanvasGroup _cvGroup;
   private void OnEnable()
   {
      BlockManager.onFullSpawnEvent += OnFlashBag;
   }

   private void OnDisable()
   {
      BlockManager.onFullSpawnEvent -= OnFlashBag;
   }

   void OnFlashBag()
   {
      _cvGroup.DOFade(0.9f, 0.2F).From(0).SetLoops(2, LoopType.Yoyo);
   }
}
