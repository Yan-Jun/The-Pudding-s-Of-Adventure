using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class EditorCreator : Editor
{
    [MenuItem("Customer/ScriptableObject/Create PlayerData")]
    static void Fn_CreateScriptableObject()
    {
        if (!Directory.Exists("Resources"))
            Directory.CreateDirectory("Assets/Resources");

        string path =  "Assets/Resources/Player.asset";
        PlayerData data = CreateInstance<PlayerData>();
        AssetDatabase.CreateAsset(data , path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
