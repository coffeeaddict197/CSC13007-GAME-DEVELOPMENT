using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : MonoBehaviour
{

    public static bool battleReady = false;
    public async void OnStartGame()
    {
        battleReady = false;
        Transition.Instance.PlayTransition("CloseAnim");
        await UniTask.DelayFrame(35);
        SceneManager.LoadSceneAsync(2);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        Transition.Instance.PlayTransition("OpenAnim");
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        battleReady = true;
    }
}
