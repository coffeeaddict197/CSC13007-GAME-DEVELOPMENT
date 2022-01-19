using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FXUpgradeClicked : MonoSingleton<FXUpgradeClicked>
{
    public List<FXElement> elements;

    #if UNITY_EDITOR

    private void OnValidate()
    {
        elements = GetComponentsInChildren<FXElement>().ToList();
    }

#endif
    
    public async void StartFX()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].DOFX();
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: this.GetCancellationTokenOnDestroy());
        }
    }
}
