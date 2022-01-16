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
}
