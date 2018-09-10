using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager ms_instance = null;

    [SerializeField]
    CanvasGroup m_canvasGroup;


    GameState m_gameState = GameState.Playing;


    public static GameState State {  get { return ms_instance.m_gameState; } }



	// Use this for initialization
	void Awake ()
    {
        // simple cheap singleton
        if (ms_instance != null)
            Debug.LogError("Only one instance of the manager is allowed");
        ms_instance = this;

        Door.DoorOpened += OnDoorOpened;
	}

    private void OnDestroy()
    {
        ms_instance = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(m_gameState == GameState.Ended)
        {
            m_canvasGroup.alpha = 1;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                m_gameState = GameState.Reloading;
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Splash");
            }

        }
    }



    private void OnDoorOpened()
    {
        m_gameState = GameState.Ended;
    }
}
