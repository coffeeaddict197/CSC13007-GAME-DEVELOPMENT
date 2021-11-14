using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFX : MonoSingleton<MonsterFX>
{
    [Header("FX Damage")]
    [SerializeField] private ParticleSystem fx_monsterTakeDamage;
    [SerializeField] private ParticleSystem fx_monsterDeath;
    
    
    public void FXPlayMonsterTakeDamage() => fx_monsterTakeDamage.Play();
    public void FXPlayMonsterDeath() => fx_monsterDeath.Play();

}
