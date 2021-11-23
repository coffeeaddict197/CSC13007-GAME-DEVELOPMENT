using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/LevelAssetsConfig", fileName = "Levelx-x")]
public class LevelAssetsConfigs : ScriptableObject
{
    #if UNITY_EDITOR

    private void OnValidate()
    {
        for (int i = 0; i < levelAssetsConfig.Count; i++)
        {
            levelAssetsConfig[i].LoadLevelConfig();
        }
    }

#endif
    public List<LevelsConfig> levelAssetsConfig;
}

[System.Serializable]
public class LevelsConfig
{
    public int map;
    public int level;
    public TextAsset levelInTextFormat;
    public List<MonsterLevelConfigs> monsterLevel;

    public void LoadLevelConfig()
    {
        monsterLevel = JsonConvert.DeserializeObject<List<MonsterLevelConfigs>>(levelInTextFormat.text);
    }
}

[System.Serializable]
public class MonsterLevelConfigs
{
    public int monsterType;
    public string name;
    public int health;
    public int gen;
}
