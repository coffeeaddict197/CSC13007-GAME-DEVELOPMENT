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
        if (toggleType == ToggleType.Music)
        {
            isOn = SoundManager.Instance.AudioData.IsOnFx;
        }
        else
        {
            isOn = SoundManager.Instance.AudioData.IsOnSoundTrack;
            Debug.LogError("Is Sound track : " + isOn);
        }
        OnButtonChange();
    }

    public void OnClickToggle()
    {
        bgSpr.sprite = SettingPanelInGame.Instance.bgToggleOff;
        isOn = !isOn;
        OnButtonChange();
    }

    public void ToggleMusic()
    {
        OnClickToggle();
        SoundManager.Instance.AudioData.IsOnFx = isOn;



    }

    public void ToggleSound()
    {
        OnClickToggle();
        SoundManager.Instance.AudioData.IsOnSoundTrack = isOn;
        if (!isOn)
            SoundManager.Instance.TurnOffAllSoundTrack();
        else
            SoundManager.Instance.TurnOnAllSoundTrack();
    }

    void OnButtonChange()
    {
        bgSpr.sprite = isOn ? SettingPanelInGame.Instance.bgToggleOn : SettingPanelInGame.Instance.bgToggleOff;
        icnRect.anchoredPosition = new Vector2(isOn ? maxWidth : 15,icnRect.anchoredPosition.y);
    }
}
