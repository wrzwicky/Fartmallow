
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tutorial))]
public class TutorialEditor : Editor
{


    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        Tutorial tutorial = (Tutorial)target;

        if (Application.isPlaying)
        {

            if (GUILayout.Button("Show"))
                tutorial.Show();

        }
    }

}
