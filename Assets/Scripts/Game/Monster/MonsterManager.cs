using System;
using System.Collections;
using System.Collections.Generic;
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
                GetCurrentMonster().Move(new Vector3(1.41f,1.76f,0f));
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

    Monster GetCurrentMonster() =>  listMonster[idxCurrentMonster];
    

    public bool IsMonsterAvailble()
    {
        var monster = GetCurrentMonster();
        if (!monster.isDeath)
        {
            return true;
        }

        return false;
    }
    
    
}
