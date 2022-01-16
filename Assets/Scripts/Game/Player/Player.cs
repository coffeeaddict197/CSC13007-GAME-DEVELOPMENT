﻿using System;
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
                _currentHealth = value;
            if (_currentHealth >= maxHealth)
                _currentHealth = maxHealth;
            _playerUI.OnHealhChange(_currentHealth,maxHealth);
        }
    }
    
    [Header("Reference")]
    public PlayerGear gears;
    public PlayerBuff spellBuff;
    [SerializeField] private Transform anchor;
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private Animator _anim;
    [SerializeField] private PlayerFX _FX;
    //Event
    public static Action<int> onPlayerDamage;
    public static Action<int> onPlayerTakeDamage;
    
    
    //---------------BUILD IN METHOD---------------
    private IEnumerator Start()
    {
        InitPlayerStat(500);
        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayerLoopAction());
    }

    private void OnEnable()
    {
        onPlayerDamage += OnPlayerDamage;
        onPlayerTakeDamage += OnPlayerTakeDamage;
    }

    private void OnDisable()
    {
        onPlayerDamage -= OnPlayerDamage;
        onPlayerTakeDamage -= OnPlayerTakeDamage;
    }

    //---------------END BUILD IN METHOD------------

    IEnumerator PlayerLoopAction()
    {
        if (!gears.isEquiped)
        {
            yield return StartCoroutine(WaitPlayerEquip());
        }
        yield return StartCoroutine(PlayerMovement());
        yield return StartCoroutine(PlayerAttack());
        _FX.FXOnCollectionCoin(1.5f);
        yield return new WaitForSeconds(1f);
        MonsterManager monsterManager = MonsterManager.Instance;
        LevelProgress.onUpdateProgress?.Invoke( monsterManager.CurrentMonster ,monsterManager.MaxMonster);
        bool isNotEndGame = !monsterManager.AllMonsterDeath() && _currentHealth > 0;
        if (isNotEndGame)
        {
            //Respawn weapon
            BlockManager.Instance.SpawnBlock();
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => !OverlootEvent.Instance.isPlayingEvent);
            StartCoroutine(PlayerLoopAction());
        }
        else
        {
            //Win or loose handler
            if (_currentHealth > 0)
            {
                StartCoroutine(WinnerHandler());
            }
            else
            {
                //Lose
            }
        }

    }

    IEnumerator WaitPlayerEquip()
    {
        yield return new WaitUntil(() => gears.isEquiped == true);
        Animator camAnim = Camera.main.GetComponent<Animator>();
        camAnim.Play("FirstCamAnim");
        yield return new WaitForSeconds(1f);
    }
    
    IEnumerator PlayerMovement(float timeMovement = 3f)
    {
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
        FX_ScreenEndGame.Instance.Excute();
        yield return StartCoroutine(PlayerMovement());
        _anim.SetTrigger("Dance");
    }

    public void InitPlayerStat(int maxHeal)
    {
        maxHealth = maxHeal;
        CurrentHealth = maxHeal;
    }

    void OnPlayerTakeDamage(int damage)
    {
        this._FX.FXPlayPlayerTakeDamage();
        damage -= spellBuff.GetBuffAffectValue(BuffEnum.BuffShield);
        damage =  damage - gears.SheildPhysicsResistant() - gears.SheildPhysicsResistant() - ShieldHandler.DefaultPhysicsRetristant();
        damage = Mathf.Clamp(damage, 1, Int32.MaxValue);
        CurrentHealth -= damage;
        FXFactory.Instance.GetFXTextFactory().SpawnFX(anchor.position,damage.ToString(),FXTextFactory.damageColor);
    }

    void OnPlayerDamage(int damage)
    {
        
    }

    public Transform PlayerAnchor => anchor;
    
    public void OnPlayerHealth(int health)
    {
        _FX.FXPlayPlayerHealth(health);
    }

    public void DisableAllStuffAndUI()
    {
        gears.UnEquipAllGear();
        _playerUI.gameObject.SetActive(false);
    }

}
