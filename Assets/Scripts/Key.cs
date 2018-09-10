using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    readonly int k_collectedKey = Animator.StringToHash("Collected");

    Animator m_animator;
    AudioSource m_audioSource;
    Collider m_collider;

    bool m_collected = false;


    public bool IsCollected {  get { return m_collected; } }

    // Use this for initialization
    void Awake ()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!m_collected)
        {
            m_collected = true;
            m_collider.enabled = false;
            m_audioSource.Play();
            m_animator.SetTrigger(k_collectedKey);
        }
    }
}
