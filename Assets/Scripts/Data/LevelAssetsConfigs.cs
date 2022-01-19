using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/LevelAssetsConfig", fileName = "Levelx-x")]
public class LevelAssetsConfigs : ScriptableObject
{
    #if UNITY_EDITOR

    private void OnValidate()
    {
        string path = "Assets/Assets/Data/LevelData/";
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles("*.json");
        levelAssetsConfig = new List<LevelsConfig>();
        for (int i = 0; i < fileInfo.Length; i++)
        {
            TextAsset jsonAsset = AssetDatabase.LoadAssetAtPath($"{path}{fileInfo[i].Name}", typeof(TextAsset)) as TextAsset; 
            LevelsConfig config = JsonConvert.DeserializeObject<LevelsConfig>(jsonAsset.text);
            levelAssetsConfig.Add(config);
        }
    }

#endif
    public List<LevelsConfig> levelAssetsConfig;

    public static LevelAssetsConfigs Instance
    {
        get => LoaderUtility.Instance.GetAsset<LevelAssetsConfigs>("Configs/LevelAssets/LevelConfigs");
    }

    public LevelsConfig GetLevel(int level)
    {
        return levelAssetsConfig.Find(x => x.level == level);
    }
}

[System.Serializable]
public class LevelsConfig
{
    public int map;
    public int level;
    public List<MonsterLevelConfigs> listMonster;
    public RewardData rewardData;
}

[System.Serializable]
public class MonsterLevelConfigs
{
    public int monsterType;
    public string name;
    public int health;
    public MonsterGen gen;
}

[System.Serializable]
public class RewardData
{
    public int wood;
    public int silver;
    public int coin;
    public int mana;
}


