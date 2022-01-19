using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ISmithLevelBooster : MonoBehaviour
{
    public static Action OnLevelUp;

    [SerializeField] private TextMeshProUGUI text;
    
    #if UNITY_EDITOR

    public string stringAppend = "";
    private void OnValidate()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

#endif


    private void OnEnable()
    {
        OnLevelUp += OnChangeLevel;
        OnChangeLevel();
    }

    private void OnDisable()
    {
        OnLevelUp -= OnChangeLevel;
    }

    void OnChangeLevel()
    {
        text.text = PlayerDataManager.Instance.data.ItemBlackSmithLevel.ToString() + stringAppend;
    }
    
}
