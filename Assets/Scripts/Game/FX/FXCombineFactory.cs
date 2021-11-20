using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FXCombineFactory : FXPool
{
    [SerializeField] private Animator fxCombineModel;

    public void Init()
    {
        Init(fxCombineModel);
        CreatePool();
    }
    
    public void SpawnFX(RectTransform rect)
    {
        var obj = GetObject();
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            obj.gameObject.SetActive(true);
            
            RectTransform rectFX = obj.transform as RectTransform;
            rectFX.sizeDelta = rect.sizeDelta;
            rectFX.transform.position = rect.transform.position;
            
        }
    }
    
    Animator GetObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            var gObj = (Animator) pool[i];
            if (!gObj.gameObject.activeSelf)
            {
                return gObj;
            }
        }
        return null;
    }

}
