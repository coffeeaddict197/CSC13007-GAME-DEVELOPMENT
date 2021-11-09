using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class FXFactory : MonoSingleton<FXFactory>
{
    public FXTextFactory fxTextFactory;
    private void Start()
    {
        fxTextFactory.Init();
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

[System.Serializable]
public class FXTextFactory : FXPool
{
    [SerializeField] private TextMeshProUGUI textModel;

    public void Init()
    {
        Init(textModel);
        CreateFX();
    }
    
    void CreateFX()
    {
        CreatePool();
    }

    public void SpawnFX(Vector3 position,string txt)
    {
        var obj = GetObject();
        if (obj != null)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            obj.text = txt;
        }
    }
    
    TextMeshProUGUI GetObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            var gObj = (TextMeshProUGUI) pool[i];
            if (!gObj.gameObject.activeSelf)
            {
                return gObj;
            }
        }
        return null;
    }
}
