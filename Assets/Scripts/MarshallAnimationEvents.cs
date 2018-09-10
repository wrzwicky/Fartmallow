using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshallAnimationEvents : MonoBehaviour {
    private PlayerController myPlayer;

    private void Awake()
    {
        myPlayer = FindObjectOfType<PlayerController>();
    }

    // Called when fart should start producing things.
    public void FartStart()
    {
        //start gas cloud
        myPlayer.DoFartEffects();
    }
}
