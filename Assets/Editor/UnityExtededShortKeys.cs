#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class UnityExtededShortKeys : ScriptableObject
{
    [MenuItem("HotKey/Run _F5")]
    static void PlayGame()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), "", false);
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    [MenuItem("HotKey/SetActiveGO _%&A")]
    static void SetActiveGO()
    {
        if (Selection.activeGameObject != null)
        {
            Selection.activeGameObject.SetActive(!Selection.activeGameObject.activeSelf);
        }
    }


    private static Vector3 tempPosition;
    private static Quaternion tempRotation;

    [MenuItem("HotKey/CopyPosRot _#&C")]
    static void CopyTransform()
    {
        if (Selection.activeGameObject != null)
        {
            tempPosition = Selection.activeGameObject.transform.position;
            tempRotation = Selection.activeGameObject.transform.rotation;
        }
    }

    [MenuItem("HotKey/PastePosRot _#&V")]
    static void PasteTransform()
    {
        if (Selection.activeGameObject != null)
        {
            Undo.RecordObject(Selection.activeGameObject.transform, "Paste position and rotation");
            Selection.activeGameObject.transform.position = tempPosition;
            Selection.activeGameObject.transform.rotation = tempRotation;
        }
    }

    [MenuItem("HotKey/CreateDestStartGO _%&N")]
    static void CreateDestStartGO()
    {
        if (Selection.activeGameObject != null)
        {
            GameObject goStart = new GameObject(Selection.activeGameObject.name + "_StartRotation");
            goStart.transform.parent = Selection.activeGameObject.transform.parent;
            GameObject goDest = new GameObject(Selection.activeGameObject.name + "_DestRotation");
            goDest.transform.parent = Selection.activeGameObject.transform.parent;

            goStart.transform.position = Selection.activeGameObject.transform.position;
            goDest.transform.position = Selection.activeGameObject.transform.position;

            goStart.transform.rotation = Selection.activeGameObject.transform.rotation;
            goDest.transform.rotation = Selection.activeGameObject.transform.rotation;

        }
    }

    [MenuItem("HotKey/ShowGlobalPosition _%&G")]
    static void ShowGlobalPosition()
    {
        if (Selection.activeGameObject != null)
        {

            Debug.Log(Selection.activeGameObject.name + Selection.activeGameObject.transform.position.ToString("F4"));
            //Selection.activeGameObject.transform.rotation = tempRotation;
        }
    }

}

#endif