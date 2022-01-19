using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerDataManager : MonoSingleton<PlayerDataManager>
{
    public PlayerData data;

    protected override void Awake()
    {
        Load();
    }
    
    private static string KEY_DATA = "PLAYER_DATA";
    
    
    //Unit of work
    public void Save()
    {
        string objToJson = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(KEY_DATA,objToJson);
    }
    
    public void Load()
    {
        string jsonData = PlayerPrefs.GetString(KEY_DATA, "NONE");
        if (jsonData.Equals("NONE"))
        {
            data.OnSetup();
            Save();  
        }
        else
        {
            data = JsonConvert.DeserializeObject<PlayerData>(jsonData);
        }
    }
    
    void OnApplicationQuit()
    {
        Debug.LogError("Onquit");
        Save();
    }
}

[System.Serializable]
public class PlayerData
{
    public PlayerLevelData LevelDatas;
    public GearDatas GearDatas;
    public PlayerCurrencyData currencyData;
    public int ItemBlackSmithLevel;

    public void OnSetup()
    {
        ItemBlackSmithLevel = 1;
        GearDatas.SetupData();
        currencyData.OnSetupData();
    }
}