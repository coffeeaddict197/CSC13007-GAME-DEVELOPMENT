using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    public int level;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private Button buttonLevel;
    [SerializeField] private Image buttonImg; 


    public static ButtonLevel buttonLevelClicked;
#if UNITY_EDITOR
    private void OnValidate()
    {
        try
        {
            txtLevel = GetComponentInChildren<TextMeshProUGUI>();
            buttonLevel = GetComponent<Button>();
            buttonImg = GetComponent<Image>();
            txtLevel.text = level.ToString();
        }
        catch (Exception e)
        {
           
        }
    }
#endif

    private void Start()
    {
        OnButtonSetup();
        buttonLevel.onClick.AddListener(OnLevelClick);
    }

    public void OnLevelClick()
    {
        buttonLevelClicked = this;
    }

    public void OnButtonSetup()
    {
        if (level < PlayerDataManager.Instance.data.LevelData.archiveLevel)
        {
            UnlockLevel();
        }
        else if(level > PlayerDataManager.Instance.data.LevelData.archiveLevel)
        {
            LockLevel();
        }
        else
        {
            
        }
    }

    void UnlockLevel()
    {
        buttonImg.sprite = LoaderUtility.Instance.GetAsset<Sprite>("UI/ButtonLevel/Done");
    }

    void LockLevel()
    {
        buttonImg.sprite = LoaderUtility.Instance.GetAsset<Sprite>("UI/ButtonLevel/Locked");
    }

    void CurrentLevel()
    {
        
    }
    
}
