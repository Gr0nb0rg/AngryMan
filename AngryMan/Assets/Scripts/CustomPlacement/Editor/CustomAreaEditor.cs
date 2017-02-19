using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomArea))]
public class CustomAreaEditor : Editor {
    CustomArea m_target;

    public override void OnInspectorGUI()
    {
        m_target = (CustomArea)target;
        DrawGenerateButton();
        DrawDefaultInspector();
    }

    void DrawGenerateButton()
    {
        if (GUILayout.Button("CreateArea"))
        {
            Undo.RecordObject(m_target, "InstantiateArea()");
            m_target.InstantiateArea(Vector3.zero);
        }
    }
}
