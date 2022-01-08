using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoSingleton<MonsterManager>
{
    [SerializeField] List<Monster> listMonster;
    public int MaxMonster { get; set; }
    public int CurrentMonster => MaxMonster - listMonster.Count;

    [Header("List Monster")] 
    public List<MonsterModel> monstersModel;

    protected override void Awake()
    {
        base.Awake();
        listMonster = new List<Monster>();
        CreateListMonster();
        MaxMonster = listMonster.Count;
    }
    

    public void CreateListMonster()
    {
        var levelConfig = LevelAssetsConfigs.LevelConfigs().GetLevel(ButtonLevel.buttonLevelClicked.level);
        for (int i = 0; i < levelConfig.listMonster.Count; i++)
        {
            var monster = levelConfig.listMonster[i];
            var monsterType = monster.monsterType;
            var monsterName = monster.name;
            var monsterHealth = monster.health;
            var monsterGen = monster.gen;
            CreateMonster((MonsterType)monsterType, monsterName, monsterHealth, monsterGen);
        }

    }
    
    Monster CreateMonster(MonsterType type, string monsterName,int health,MonsterGen gen)
    {
        Monster model = monstersModel.Find(x => x.monsterType == type)?.GetMonster(monsterName);
        if (model != null)
        {
            Monster newMons = Instantiate(model, new Vector3(4, Player.Instance.transform.position.y, 0), Quaternion.identity, this.transform);
            newMons.Init(health,gen);
            listMonster.Add(newMons);
            return newMons;
        }

        return null;
    }

    public Monster GetCurrentMonster()
    {
        if (listMonster.Count > 0)
        {
            return listMonster[0];
        }
        return null;
    }

    public void RemoveMonster(Monster m)
    {
        if (listMonster.Contains(m))
        {
            listMonster.Remove(m);
        }
    }
    

    public bool IsMonsterAvailble()
    {
        var monster = GetCurrentMonster();
        if (!monster.isDeath)
        {
            return true;
        }
        return false;
    }

    public bool AllMonsterDeath()
    {
        return listMonster.Count == 0;
    }

    public bool IsBoss(Monster m) //LastMonster
    {
        if (listMonster.IndexOf(m) == listMonster.Count - 1)
        {
            return true;
        }
        return false;
    }
    
    
}


public enum MonsterType
{
    Slime = 1,
    Dino = 2,
    Dragon = 3
}

[Serializable]
public class MonsterModel
{
    public MonsterType monsterType;
    public List<Monster> monstersModel;

    public Monster GetMonster(string monsterName)
    {
        Monster model = monstersModel.Find(x => x.monsterName == monsterName);
        if (model != null)
        {
            return model;
        }
        return null;
    }
}
