using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [Header("Beam FX")] 
    [SerializeField] private ParticleSystem fx_playerTakeDamage;

    public void FXPlayPlayerTakeDamage() => fx_playerTakeDamage.Play();
    
}
