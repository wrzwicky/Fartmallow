﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    readonly int k_openKey = Animator.StringToHash("Open");

    [SerializeField]
    bool m_requiresKey = false;

    Animator m_animator;
    
    public bool RequiresKey {  get { return m_requiresKey; } }

    public bool IsOpen {  get { return m_animator.GetBool(k_openKey); } }

	// Use this for initialization
	void Awake ()
    {
        m_animator = GetComponent<Animator>();
	}
	
	public void Open()
    {
        m_animator.SetBool(k_openKey, true);
    }

    public void Close()
    {
        m_animator.SetBool(k_openKey, false);
    }
}