using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [Header("Beam FX")] 
    [SerializeField] private ParticleSystem fx_playerTakeDamage;
    [Header("Health FX")]
    [SerializeField] private ParticleSystem fx_playerHealth;

    public void FXPlayPlayerTakeDamage() => fx_playerTakeDamage.Play();

    public void FXPlayPlayerHealth(int heath)
    {
        fx_playerHealth.Play();
        FXFactory.Instance.GetFXTextFactory().SpawnFX(Player.Instance.PlayerAnchor.position,heath.ToString(),FXTextFactory.healthColor);
    }
}
