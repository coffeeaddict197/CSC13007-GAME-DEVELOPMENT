using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
        
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI _textHelth;
    [SerializeField] private Image _fillHealth;

    public void OnHealhChange(int currenHeal,int maxHealh)
    {
        _fillHealth.fillAmount = (float) currenHeal / maxHealh;
        _textHelth.text = currenHeal.ToString();
    }
}
