using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{


    public override void OnInspectorGUI()
    {
        Door door = (Door)target;

        if(Application.isPlaying)
        {
            if (door.IsOpen)
            {
                if (GUILayout.Button("Close"))
                    door.Close();
            }
            else
            {
                if (GUILayout.Button("Open"))
                    door.Open();
            }
        }
    }

}
