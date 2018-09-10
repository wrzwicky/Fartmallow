

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{


    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        PlayerController player = (PlayerController)target;

        if (Application.isPlaying)
        {

            if (GUILayout.Button("Feed"))
                player.Feed();
            if (GUILayout.Button("Gift Key"))
                player.GiftKey();
        }
    }

}

