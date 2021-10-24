using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using  TMPro;

public enum MonsterGen
{
    Fire,
    Ice,
    Earth,
    Lighting
}
public class Monster : MonoBehaviour
{

    [SerializeField] private int _maxHealth;
    [SerializeField] private MonsterGen _monsterGen;
    [SerializeField] int _health = 0;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI _textHelth;
    [SerializeField] private Image _fillHealth;
    
    [Header("Stats")]
    public string monsterName;
    public bool isDeath;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            _textHelth.text = _health.ToString();
            _fillHealth.fillAmount = (float) _health / _maxHealth;

        }
    }

    private void OnEnable()
    {
        _health = _maxHealth;
        _textHelth.text = _maxHealth.ToString();
        Player.onPlayerTakeDamagae += OnMonsterTakeDamage;
    }

    private void OnDisable()
    {
        Player.onPlayerTakeDamagae -= OnMonsterTakeDamage;
    }

    public void Init(int health, MonsterGen gen)
    {
        _maxHealth = health;
        _monsterGen = gen;
    }

    void OnMonsterTakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            isDeath = true;
        }
    }

    public void Move(Vector3 position)
    {
        transform.DOMove(position, 0.5f);
    }
    
}
