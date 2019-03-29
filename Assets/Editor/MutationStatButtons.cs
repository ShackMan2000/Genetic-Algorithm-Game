using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MutationStat))]
public class MutationStatsButtons : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("ResetStat"))
        {
            (target as MutationStat).ResetValues();
        }
    }


}