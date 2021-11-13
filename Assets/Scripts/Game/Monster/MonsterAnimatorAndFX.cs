using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimatorAndFX : MonoBehaviour
{
    [Header("Reference")] 
    [SerializeField] private Monster monster;
    
    #if UNITY_EDITOR

    private void OnValidate()
    {
        monster = transform.parent.GetComponent<Monster>();
    }

#endif
    
    public void OnPlayFX()
    {
        
    }

    public void OnAttackPlayer()
    {
        monster.onMonsterAttack?.Invoke();
    }
}
