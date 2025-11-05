using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class UsefulTool : EditorWindow
{
    #region SceneLoader

    private bool _showSceneLoader;
    private string[] _onListScenes;
    private string[] _outListScenes;
    private Vector2 _onListScroll;
    private Vector2 _outListScroll;
    private Vector2 _sceneLoaderScroll;

    #endregion

    [MenuItem("Useful Tools/Main Window")]
    public static void ShowWindow()
    {
        GetWindow<UsefulTool>("Useful Tool");
    }

    private void OnGUI()
    {
        _showSceneLoader = EditorGUILayout.Foldout(_showSceneLoader, "Show Scene Loader", true);
        if (_showSceneLoader)
        {
            SceneLoader();
        }
    }

    private void SceneLoader()
    {
        _sceneLoaderScroll = EditorGUILayout.BeginScrollView(_sceneLoaderScroll, GUILayout.Height(300));
        if (GUILayout.Button("シーンリストを更新"))
        {
            _onListScenes = Enum.GetNames(typeof(InListSceneName));
            _outListScenes = Enum.GetNames(typeof(OutListSceneName));
        }

        if (_onListScenes == null) return;
        // 左側
        EditorGUILayout.LabelField("On List Scenes", EditorStyles.boldLabel);

        if (_onListScenes != null)
        {
            foreach (var scene in _onListScenes)
            {
                if (GUILayout.Button(scene))
                {
                    var canSwitch = SwitchSceneByName(scene);
                    if (canSwitch)
                    {
                        EditorGUILayout.EndScrollView();
                        SceneEnumGenerate.Generate();
                        _onListScenes = Enum.GetNames(typeof(InListSceneName));
                        _outListScenes = Enum.GetNames(typeof(OutListSceneName));
                        return;
                    }
                }
            }
        }


        // 右側
        EditorGUILayout.LabelField("Out List Scenes", EditorStyles.boldLabel);

        if (_outListScenes != null)
        {
            foreach (var scene in _outListScenes)
            {
                if (GUILayout.Button(scene))
                {
                    var canSwitch = SwitchSceneByName(scene);
                    if (canSwitch)
                    {
                        EditorGUILayout.EndScrollView();
                        SceneEnumGenerate.Generate();
                        _onListScenes = Enum.GetNames(typeof(InListSceneName));
                        _outListScenes = Enum.GetNames(typeof(OutListSceneName));
                        return;
                    }
                }
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private bool SwitchSceneByName(string sceneName)
    {
        // シーンのアセットパスを取得
        string[] guids = AssetDatabase.FindAssets(sceneName + " t:Scene");
        if (guids.Length == 0)
        {
            return false;
        }

        // 最初に見つかったシーンを開く
        string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);

        // 現在のシーンを保存してから開く
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath);
        }

        return true;
    }
}