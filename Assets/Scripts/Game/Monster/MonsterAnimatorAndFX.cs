using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimatorAndFX : MonoBehaviour
{
    [Header("Reference")] 
    [SerializeField] private Monster monster;
    public void OnPlayFX()
    {
        
    }

    public void OnAttackPlayer()
    {
        monster.onMonsterAttack?.Invoke();
    }
}
