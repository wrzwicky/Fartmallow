
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Block block = (Block)target;

        if (Application.isPlaying)
        {

                if (GUILayout.Button("Move"))
                    block.Push();

        }
    }

}

