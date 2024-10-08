﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using  TMPro;
using Random = UnityEngine.Random;

public enum MonsterGen
{
    Fire,
    Ice,
    Earth,
    Lighting
}
public class Monster : MonoBehaviour
{

    
    [Header("Stats")]
    public string monsterName;
    public int monsterDamage;
    public bool isDeath;
    
    protected int _maxHealth;
    protected MonsterGen _monsterGen;
    protected int _health = 0;

    [Header("Animator")] 
    [SerializeField] private Animator _anim;
    
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI _textHelth;
    [SerializeField] private Image _fillHealth;
    [SerializeField] private Image _bgHealth;
    [SerializeField] private Transform anchorPosition;
    
    public Action onMonsterAttack;
    
    public int Health
    {
        get => _health;
        set
        {
            _health = value;

            if (_health < 0)
            {
                isDeath = true;
                _anim.SetTrigger("Die");
                _health = 0;
            }
            
            _textHelth.text = _health.ToString();
            _fillHealth.fillAmount = (float) _health / _maxHealth;

        }
    }

    private void Start()
    {
        if (MonsterManager.Instance.IsBoss(this))
        {
            _fillHealth.sprite = LoaderUtility.Instance.GetAsset<Sprite>("UI/HealthBar/BossFill");
            _bgHealth.sprite = LoaderUtility.Instance.GetAsset<Sprite>("UI/HealthBar/BossBar");
        }
    }


    //---------------BUILD IN METHOD-----------------
    private void OnEnable()
    {
        this.onMonsterAttack += OnMonsterAttack;
        Player.onPlayerDamage += OnMonsterTakeDamage;
    }

    private void OnDisable()
    {
        this.onMonsterAttack -= OnMonsterAttack;
        Player.onPlayerDamage -= OnMonsterTakeDamage;
    }

    public void Init(int health, MonsterGen gen)
    {
        _maxHealth = health;
        Health = _maxHealth;
        _monsterGen = gen;
    }
    
    //---------------END BUILD IN METHOD-----------------

    public void MonsterAction() => StartCoroutine(MonsterCoreAction());
    
    IEnumerator MonsterCoreAction()
    {
        yield return StartCoroutine(MonsterStartMove());
        yield return StartCoroutine(MonsterAttack());
        yield return StartCoroutine(MonsterDie());
    }

    public  virtual IEnumerator MonsterStartMove()
    {
        Move(new Vector3(1.41f,Player.Instance.transform.position.y,0f));
        yield return new WaitForSeconds(1f);
    }
    
    public  virtual IEnumerator MonsterAttack()
    {
        _anim.SetTrigger("Attack");
        while (Health > 0)
        {
            if(Player.Instance.IsDeath)
            {
                _anim.Play("Idle");
                yield return new WaitUntil(() => !Player.Instance.IsDeath);
                _anim.SetTrigger("Attack");
            }
            yield return null;
        }
    }
    
    IEnumerator MonsterDie()
    {
        SoundManager.Instance.Play("Monster_Death",AudioType.FX,0.6F);
        _anim.SetTrigger("Die");
        yield return new WaitForSeconds(0.5f);
        MonsterFX.Instance.FXPlayMonsterDeath();
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
        MonsterManager.Instance.RemoveMonster(this);
    }


    protected virtual void OnMonsterAttack()
    {
        SoundManager.Instance.Play("Monster_Attack",AudioType.FX,0.6F);
        MonsterFX.Instance.FXPlayMonsterAttack();
        PlayerGear gear = Player.Instance.gears;
        int damage = Mathf.Abs(Ultility.RandomIn(monsterDamage,monsterDamage-5,monsterDamage+5));
        Player.onPlayerTakeDamage(damage);
        gear.AffectShieldDurability((float)monsterDamage / 2);
        gear.AffectHelmetDurability((float)monsterDamage / 3);
    }
    
    protected virtual void OnMonsterTakeDamage(int damage)
    {
        if (this != MonsterManager.Instance.GetCurrentMonster())
            return;
        SoundManager.Instance.Play("Monster_Hit",AudioType.FX,0.6F);
        Health -= damage;
        MonsterFX.Instance.FXPlayMonsterTakeDamage();
        FXFactory.Instance.GetFXTextFactory().SpawnFX(GetAnchorPosition(),damage.ToString(),FXTextFactory.damageColor);
        Debug.LogError("Spawn");
    }

    public void Move(Vector3 position) => transform.DOMove(position, 0.5f);

    public Vector3 GetAnchorPosition() => anchorPosition.position;

}
