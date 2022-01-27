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


    public static int levelClicked;
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
        var levelArchive = PlayerDataManager.Instance.data.LevelDatas.archiveLevel;
        
        if (level != levelArchive)
            return;
        
        levelClicked = level;
        DialogManager.Instance.OnShowDialog<BaseDialog>("Dialog/Home/LevelInfo",
            DialogType.DialogWithoutNavigate);
    }

    public void OnButtonSetup()
    {
        if (level < PlayerDataManager.Instance.data.LevelDatas.archiveLevel)
            UnlockLevel();
        else if(level > PlayerDataManager.Instance.data.LevelDatas.archiveLevel)
            LockLevel();
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
