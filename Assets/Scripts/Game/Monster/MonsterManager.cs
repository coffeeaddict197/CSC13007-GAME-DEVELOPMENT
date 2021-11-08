using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoSingleton<MonsterManager>
{
    [SerializeField] List<Monster> listMonster;

    [Header("List Monster")] 

    public List<Monster> modelMonster;
    
    private void Start()
    {
        listMonster = new List<Monster>();
        CreateListMonster();
    }

    public void CreateListMonster()
    {
        
        CreateMonster("Yellow Slime", 200, MonsterGen.Earth);
        CreateMonster("Yellow Slime", 200, MonsterGen.Earth);

    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     if (idxCurrentMonster < listMonster.Count)
        //     {
        //         GetCurrentMonster().MonsterAction();
        //     }
        // }
    }

    Monster CreateMonster(string monsterName,int health,MonsterGen gen)
    {
        Monster model = modelMonster.Find(x => x.monsterName == monsterName);
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
