using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterAssets",fileName = "MonsterAssetConfig")]
public class MonsterAssets : ScriptableObject
{
    public List<MonsterAsset> monsterAssetsConfig;
    public List<GenAsset> monsterGenAssets;

    public MonsterAsset GetMonsterAsset(string monsterName)
    {
        return monsterAssetsConfig.Find(x => x.name == monsterName);
    }
    
    public GenAsset GetMonsterGenAsset(MonsterGen type)
    {
        return monsterGenAssets.Find(x => x.gen == type);
    }
}

[Serializable]
public class MonsterAsset
{
    public string name;
    public Sprite sprRepresent;
}

[Serializable]
public class GenAsset
{
    public MonsterGen gen;
    public Sprite sprRepresent;
}
