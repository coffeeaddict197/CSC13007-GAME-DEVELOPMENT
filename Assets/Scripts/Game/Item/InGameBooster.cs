using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InGameBooster : MonoBehaviour
{
    [SerializeField] private SkillHandler _handler;
    [SerializeField] private ParticleSystem fx;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        fx.Play();
        _handler.OnEquipItem(null);
    }
}
