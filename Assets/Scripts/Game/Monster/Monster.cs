using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterGen
{
    Fire,
    Ice,
    Earth,
    Lighting
}
public class Monster : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private MonsterGen _monsterGen;

    public void Init(int health, MonsterGen gen)
    {
        _health = health;
        _monsterGen = gen;
    }
    
}
