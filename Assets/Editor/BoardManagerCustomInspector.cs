using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BrickManager))]
public class BoardManagerCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BrickManager boardManager = (BrickManager)target;
        if(GUILayout.Button("Create test board"))
        {
            boardManager.SpawnTestBoard();
        }
    }
}
