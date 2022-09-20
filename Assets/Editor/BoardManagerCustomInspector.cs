using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardManager))]
public class BoardManagerCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BoardManager boardManager = (BoardManager)target;
        if(GUILayout.Button("Create Board"))
        {
            boardManager.SpawnBoard();
        }
    }
}
