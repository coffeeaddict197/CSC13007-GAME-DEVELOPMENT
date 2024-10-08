﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoSingleton<PlayerFX>
{
    [Header("Beam FX")] 
    [SerializeField] private ParticleSystem fx_playerTakeDamage;
    [Header("Skill FX")]
    [SerializeField] private ParticleSystem fx_playerHealth;
    [SerializeField] private ParticleSystem fx_shieldPosion;
    [SerializeField] private ParticleSystem fx_attackPosion;
    

    [Header("Coin FX")] 
    [SerializeField] private ParticleSystemForceField fx_ForceField;
    [SerializeField] private ParticleSystem fx_collect;
    [SerializeField] private ParticleSystem fx_coinDrop;

    [Header("Skill FX")] [SerializeField] private ParticleSystem fx_FireSkill;
    
    
    public void FXPlayPlayerTakeDamage() => fx_playerTakeDamage.Play();

    public void FXPlayPlayerHealth(int heath)
    {
        fx_playerHealth.Play();
        FXFactory.Instance.GetFXTextFactory().SpawnFX(Player.Instance.PlayerAnchor.position,heath.ToString(),FXTextFactory.healthColor);
    }

    public void FXOnCollectionCoin(float delay)
    {
        StartCoroutine(IE_CollectCoin(delay));
    }

    IEnumerator IE_CollectCoin(float delay)
    {
        fx_coinDrop.Play();
        yield return new WaitForSeconds(delay);
        fx_ForceField.gameObject.SetActive(true);

        for (int i = 0; i < 10; i++)
        {
            fx_collect.Play();
            yield return new WaitForSeconds(0.2f);
        }
        
        fx_ForceField.gameObject.SetActive(false);
    }

    public void FXPlayShieldPoison() => fx_shieldPosion.Play();
    public void FXStopShieldPoison() => fx_shieldPosion.Stop();
    
    public void FXPlayAttackPoison() => fx_attackPosion.gameObject.SetActive(true);
    public void FXStopAttackPoison() => fx_attackPosion.gameObject.SetActive(false);

    public void FXPlayFireSkill() => fx_FireSkill.Play();

}
