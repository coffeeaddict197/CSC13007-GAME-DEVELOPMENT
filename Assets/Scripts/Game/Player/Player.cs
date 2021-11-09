using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    [Header("Stat")] 
    public int maxHealth;
    private int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (value >= 0)
            {
                _currentHealth = value;
            }

            if (_currentHealth >= maxHealth)
            {
                _currentHealth = maxHealth;
            }
            
            _playerUI.OnHealhChange(_currentHealth,maxHealth);
        }
    }
    
    [Header("Reference")]
    public PlayerGear gears;
    [SerializeField] PlayerUI _playerUI;
    [SerializeField] private Animator _anim;
    //Event
    public static Action<int> onPlayerDamage;
    
    
    //---------------BUILD IN METHOD---------------
    private IEnumerator Start()
    {
        InitPlayerStat(500);
        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayerLoopAction());
    }
    
    //---------------END BUILD IN METHOD------------

    IEnumerator PlayerLoopAction()
    {
        yield return StartCoroutine(PlayerMovement());
        
        yield return StartCoroutine(PlayerAttack());
        
        bool isNotEndGame = !MonsterManager.Instance.AllMonsterDeath() && _currentHealth > 0;
        // isNotEndGame = MonsterManager.Instance.AllMonsterDeath()
        Debug.Log(isNotEndGame);
        if (isNotEndGame)
        {
            //Respawn weapon
            SpawnStrategy.Instance.SpawnBlock();
            yield return new WaitForSeconds(1f);
            StartCoroutine(PlayerLoopAction());
        }

    }
    
    IEnumerator PlayerMovement()
    {
        float timeMovement = 3f;
        float curTime = 0;
        _anim.SetTrigger("Run");
        
        while (curTime < timeMovement)
        {
            curTime += Time.deltaTime;
            BackgroundScroll.Instance.UpdateBG();
            yield return null;
        }
        
        _anim.SetTrigger("Reset");
        yield return null;
    }
    
    IEnumerator PlayerAttack()
    {
        MonsterManager.Instance.GetCurrentMonster().MonsterAction();
        yield return new WaitForSeconds(1f);
        _anim.SetTrigger("Attack");
        
        while (MonsterManager.Instance.IsMonsterAvailble())
        {
            if(_currentHealth < 0)
            {
                PlayerDie();
                yield break;
            }
            yield return null;
        }
        
        _anim.SetTrigger("Reset");
        yield return new WaitForSeconds(1f);
    }
    
    void PlayerDie()
    {
        _anim.SetTrigger("Die");
    }
    
    IEnumerator WinnerHandler()
    {
        bool isClear = MonsterManager.Instance.AllMonsterDeath();
        if (isClear)
        {
            float timeMovement = 5f;
            float curTime = 0;
            _anim.SetTrigger("Run");
        
            while (curTime < timeMovement)
            {
                curTime += Time.deltaTime;
                BackgroundScroll.Instance.UpdateBG();
                yield return null;
            }
            _anim.SetTrigger("Dance");
        }
    }

    public void InitPlayerStat(int maxHeal)
    {
        maxHealth = maxHeal;
        CurrentHealth = maxHeal;
    }

}
