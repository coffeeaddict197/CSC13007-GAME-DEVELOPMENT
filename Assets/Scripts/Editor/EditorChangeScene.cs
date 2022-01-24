using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorChangeScene
{
    [MenuItem("Editor/Scene/Loading")]
    private static void ChangeLoadingScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/---Scenes---/LoadingScene.unity");
    }
    
    [MenuItem("Editor/Scene/Home")]
    private static void ChangeHomeScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/---Scenes---/HomeScene.unity");
    }
    
    [MenuItem("Editor/Scene/Game")]
    private static void ChangeGameScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/---Scenes---/Gameplay.unity");
    }
    
    
    [MenuItem("Editor/Booster/Add Coin")]
    private static void Add5000Coin()
    {
        PlayerDataManager.Instance.data.currencyData.AddData(RewardType.Coin,5000);
    }
    
    [MenuItem("Editor/Booster/Add Mana")]
    private static void Add5000Mana()
    {
        PlayerDataManager.Instance.data.currencyData.AddData(RewardType.Mana,5000);
    }
    
    [MenuItem("Editor/Booster/Add Iron")]
    private static void Add5000Iron()
    {
        PlayerDataManager.Instance.data.currencyData.AddData(RewardType.Iron,5000);
    }
    
    [MenuItem("Editor/Booster/Add Wood")]
    private static void Add5000Wood()
    {
        PlayerDataManager.Instance.data.currencyData.AddData(RewardType.Wood,5000);
    }
}
