#if UNITY_EDITOR
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class OnUnityLoad
{



    static OnUnityLoad()
    {
        EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
    }

    private static void EditorApplication_playModeStateChanged(PlayModeStateChange obj)
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
        {

            Debug.Log("Auto-Saving scene before entering Play mode: ");
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
            //EditorSceneManager.save
            //EditorApplication.SaveScene();
            //EditorApplication.SaveAssets();
        }
    }
}

#endif