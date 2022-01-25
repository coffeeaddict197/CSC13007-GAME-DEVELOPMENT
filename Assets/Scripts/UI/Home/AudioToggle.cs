using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    public enum ToggleType
    {
        Music,
        Sounds
    }

    public ToggleType toggleType;
    public Image bgSpr;
    public RectTransform icnRect;
    public int maxWidth;

    private bool isOn;

    private void OnEnable()
    {
        isOn = true;
        OnButtonChange();
    }

    public void OnClickToggle()
    {
        bgSpr.sprite = SettingPanelInGame.Instance.bgToggleOff;
        isOn = !isOn;
        OnButtonChange();
    }

    void ToggleMusic()
    {
        
    }

    void ToggleSound()
    {
        
    }

    void OnButtonChange()
    {
        bgSpr.sprite = isOn ? SettingPanelInGame.Instance.bgToggleOn : SettingPanelInGame.Instance.bgToggleOff;
        icnRect.anchoredPosition = new Vector2(isOn ? maxWidth : 15,icnRect.anchoredPosition.y);
    }
}
