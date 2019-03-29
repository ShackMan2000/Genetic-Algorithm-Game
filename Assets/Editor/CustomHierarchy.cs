using System;
using System.Linq;
using UnityEditor;
using UnityEngine;


[InitializeOnLoad]
public class CustomHierarchy : MonoBehaviour
{

    //private static Vector2 offset = new Vector2(0, 2);

    //static CustomHierarchy()
    //{
    //    EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    //}

    //private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    //{
    //    Color fontColor = Color.blue;
    //    Color background = new Color(.76f, .76f, .76f);


    //    var obj = EditorUtility.InstanceIDToObject(instanceID);
    //    if (obj != null)
    //    {



    //        var prefabType = PrefabUtility.GetPrefabType(obj);
    //        if (prefabType == PrefabType.PrefabInstance)
    //        {
    //            if (Selection.instanceIDs.Contains(instanceID))
    //            {
    //                fontColor = Color.white;
    //                background = new Color(0.24f, .48f, 0.9f);
    //            }

    //            Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
    //            EditorGUI.DrawRect(selectionRect, background);
    //            EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
    //            {
    //                normal = new GUIStyleState() { textColor = fontColor },
    //                fontStyle = FontStyle.Bold

    //            }
    //            );






    //        }
    //    }
    //}



}
