using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoSingleton<MonsterManager>
{
    private List<Monster> listMonster;

    [Header("List Monster")] 
    private int idxCurrentMonster = 0;

    public List<Monster> modelMonster;
    
    private void Start()
    {
        listMonster = new List<Monster>();
        CreateMonster("Yellow Slime", 600, MonsterGen.Earth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (idxCurrentMonster < listMonster.Count)
            {
                GetCurrentMonster().MonsterAction();
            }
        }
    }

    Monster CreateMonster(string monsterName,int health,MonsterGen gen)
    {
        Monster model = modelMonster.Find(x => x.monsterName == monsterName);
        if (model != null)
        {
            Monster newMons = Instantiate(model, new Vector3(4, 1.76f, 0), Quaternion.identity, this.transform);
            newMons.Init(health,gen);
            listMonster.Add(newMons);
            return newMons;
        }
        return null;
    }

    Monster GetCurrentMonster()
    {
        if (listMonster.Count > 0 && idxCurrentMonster < listMonster.Count)
        {
            return listMonster[idxCurrentMonster];
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
