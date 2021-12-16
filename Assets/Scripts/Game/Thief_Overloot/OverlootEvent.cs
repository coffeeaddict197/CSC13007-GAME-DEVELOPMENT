using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlootEvent : MonoSingleton<OverlootEvent>
{
    public static Action onStartOverlootEvent;
    public bool isPlayingEvent;
    [Header("Overloot GUI")] 
    [SerializeField] private ThiefOverloot overlootGUI;
    [SerializeField] private OverlootFX overlootFX;

    [Header("Thief")] 
    [SerializeField] private Thief thief;
    [SerializeField] private ScreenThief _screenThief;

    public void SpawnThief()
    {
        StartCoroutine(thief.ThiefAppear());
    }
    public void OnNotifyOverlootEvent()
    {
        WarningFullSlot.Instance.StopFX();
        overlootGUI.gameObject.SetActive(true);
        StartCoroutine(Overloot());
    }

    IEnumerator Overloot()
    {
        overlootFX.gameObject.SetActive(true);
        overlootGUI.ActiveCarousel();
        yield return StartCoroutine(overlootFX.DOFx(1.5f));
        _screenThief.gameObject.SetActive(true);
        yield return StartCoroutine(overlootGUI.IELootAction(() =>
        {
            _screenThief.gameObject.SetActive(false);
        }));

        yield return new WaitForSeconds(1F);
        StartCoroutine(thief.ThiefStartLoot());
    }
    
}
