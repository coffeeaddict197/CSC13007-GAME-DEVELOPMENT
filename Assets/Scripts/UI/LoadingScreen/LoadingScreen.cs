using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLoad;
    private void Start()
    {
        LoadScene();
    }

    async void LoadScene()
    {
        float timeLoad = 3f;
        float curTime = 0;
        while (curTime < timeLoad)
        {
            string text = ((int) ((curTime / timeLoad) * 100f)).ToString();
            textLoad.text = text + "%";
            curTime += Time.deltaTime + Random.Range(0.001f,0.005f);
            await UniTask.NextFrame();
        }
        var scene =  SceneManager.LoadSceneAsync(1); //Load to home scene
    }
}
