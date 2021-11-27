using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFX : MonoSingleton<MonsterFX>
{
    [Header("FX Damage")]
    [SerializeField] private ParticleSystem fx_monsterTakeDamage;
    [SerializeField] private ParticleSystem fx_monsterDeath;
    [SerializeField] private ParticleSystem fx_monsterAttack;
    
    
    public void FXPlayMonsterTakeDamage() => fx_monsterTakeDamage.Play();
    public void FXPlayMonsterDeath() => fx_monsterDeath.Play();
    public void FXPlayMonsterAttack() => fx_monsterAttack.Play();

}
