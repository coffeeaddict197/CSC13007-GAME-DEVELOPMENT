using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    [Header("Stat")] 
    public int maxHealth;
    private int _currentHealth;
    private bool isDie;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (value >= 0)
                _currentHealth = value;
            else
            {
                _currentHealth = 0;
            }
            if (_currentHealth >= maxHealth)
                _currentHealth = maxHealth;
            _playerUI.OnHealhChange(_currentHealth,maxHealth);
        }
    }

    public bool IsDeath
    {
        get => isDie;
        set => isDie = value;
    }
    
    [Header("Reference")]
    public PlayerGear gears;
    public PlayerBuff spellBuff;
    [SerializeField] private Transform anchor;
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private Animator _anim;
    [SerializeField] private PlayerFX _FX;

    [SerializeField] private FX_DeathScreen _fxDeathScreen;
    //Event
    public static Action<int> onPlayerDamage;
    public static Action<int> onPlayerTakeDamage;
    
    
    //---------------BUILD IN METHOD---------------
    private IEnumerator Start()
    {
        InitPlayerStat(maxHealth);
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
            CollectCoin();
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
            if(_currentHealth <= 0)
            {
                PlayerDie();
                yield return new WaitUntil(() => !IsDeath);
                CurrentHealth = maxHealth;
                _anim.SetTrigger("Attack");

            }
            yield return null;
        }
        
        _anim.SetTrigger("Reset");
        yield return new WaitForSeconds(1f);
    }
    
    void PlayerDie()
    {
        _fxDeathScreen.gameObject.SetActive(true);
        isDie = true;
        _anim.SetTrigger("Die");
    }
    
    IEnumerator WinnerHandler()
    {
        FX_ScreenEndGame.Instance.Excute();
        yield return StartCoroutine(PlayerMovement());
        _anim.SetTrigger("Dance");
    }

    async void CollectCoin()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
        for (int i = 0; i < 10; i++)
        {
            SoundManager.Instance.Play("CoinPickup",AudioType.FX,0.6f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1F));
        }
    }

    public void InitPlayerStat(int maxHeal)
    {
        maxHeal += DefaultHealth();
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

    public static int DefaultHealth()
    {
        var assets = GearItemAssets.Instance.GetAsset(GearType.Bracer);
        var data = PlayerDataManager.Instance.data.GearDatas.GetDataByType(GearType.Bracer);
        return assets.valueAfterLevelup * data.level;
    }

}
