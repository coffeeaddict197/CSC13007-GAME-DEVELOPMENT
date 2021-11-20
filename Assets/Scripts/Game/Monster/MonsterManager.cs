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
    
    private void Start()
    {
        listMonster = new List<Monster>();
        CreateListMonster();
        MaxMonster = listMonster.Count;
    }

    public void CreateListMonster()
    {
        CreateMonster(MonsterType.Slime, "Yellow Slime", 200, MonsterGen.Earth);
        CreateMonster(MonsterType.Dragon,"Blue Dragon", 200, MonsterGen.Earth);
        CreateMonster(MonsterType.Dragon,"Blue Dragon", 200, MonsterGen.Earth);
        CreateMonster(MonsterType.Dragon,"Blue Dragon", 200, MonsterGen.Earth);
        CreateMonster(MonsterType.Dragon,"Blue Dragon", 200, MonsterGen.Earth);
        CreateMonster(MonsterType.Dragon,"Blue Dragon", 200, MonsterGen.Earth);
    }
    
    Monster CreateMonster(MonsterType type, string monsterName,int health,MonsterGen gen)
    {
        Monster model = monstersModel.Find(x => x.monsterType == type)?.GetMonster(monsterName);
        if (model != null)
        {
            Monster newMons = Instantiate(model, new Vector3(4, 2.196f, 0), Quaternion.identity, this.transform);
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
    
    
}


public enum MonsterType
{
    Slime,
    Dino,
    Dragon
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
