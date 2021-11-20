using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class FXFactory : MonoSingleton<FXFactory>
{
    [Header("FX Text Take Damage")]
    [SerializeField] FXTextFactory fxTextFactory;
    public FXTextFactory GetFXTextFactory() => fxTextFactory;
    
    [Header("FX Combine factory")]
    
    [SerializeField] FXCombineFactory fxCombineFactory;
    public FXCombineFactory GetFXCombineFactory() => fxCombineFactory;

    private void Start()
    {
        fxTextFactory.Init();
        fxCombineFactory.Init();
    }
}

[System.Serializable]
public class FXPool
{
    [SerializeField] protected int numberOfPool;
    protected List<object> pool;
    protected Object originalObj;

    protected void Init(Object originObj)
    {
        pool = new List<object>();
        originalObj = originObj;
    }
    
    protected void CreatePool()
    {
        for (int i = 0; i < numberOfPool; i++)
        {
            Object newObj = GameObject.Instantiate(originalObj,FXFactory.Instance.transform);
            // GameObject gObj = (GameObject) newObj;
            // gObj.SetActive(false);
            pool.Add(newObj);
        }
    }
    
}
