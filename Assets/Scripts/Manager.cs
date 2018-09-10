using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager ms_instance = null;



	// Use this for initialization
	void Awake ()
    {
        // simple cheap singleton
        if (ms_instance != null)
            Debug.LogError("Only one instance of the manager is allowed");
        ms_instance = this;


	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
