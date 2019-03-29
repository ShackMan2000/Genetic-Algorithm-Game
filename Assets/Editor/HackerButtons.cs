using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Hacker))]
public class HackerButtons :Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("ResetMutationStats"))
        {
            (target as Hacker).WipeSaveData();
        }
    }


}
